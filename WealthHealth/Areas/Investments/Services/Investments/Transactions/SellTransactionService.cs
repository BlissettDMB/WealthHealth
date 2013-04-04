using System;
using System.Collections.Generic;
using System.Linq;
using WealthHealth.Models.Investments;
using WealthHealth.Repositories;
using WealthHealth.Repositories.Investments;
using WealthHealth.Services.StateManagement;
using WealthHealth.ViewModels.Investments.Transactions;

namespace WealthHealth.Areas.Investments.Services.Investments.Transactions
{
    public class SellTransactionService : ISellTransactionService
    {
        public readonly IPurchaseTransactionRepository PurchaseTransactionRepository;
        public readonly ISellTransactionRepository SellTransactionRepository;
        public readonly ActiveUserService ActiveUserService;

        public SellTransactionService(
            IPurchaseTransactionRepository purchaseTransactionRepository,
            ISellTransactionRepository sellTransactionRepository
        )
        {
            PurchaseTransactionRepository = purchaseTransactionRepository;
            SellTransactionRepository = sellTransactionRepository;
            ActiveUserService = new ActiveUserService();
        }

        public SellTransaction GetSellTransaction(int sellTransactionId)
        {
            throw new NotImplementedException();
        }

        public int CreateSellTransaction(AddSellTransactionVm addSellTransactionVm)
        {
            var shareQuantitySold = addSellTransactionVm.ShareQuantity;
            var sellUnitPrice = addSellTransactionVm.UnitPrice;
            var sellCommission = addSellTransactionVm.Commission;
            var sellDate = addSellTransactionVm.TransactionDate;

            decimal totalNetProfit = 0;
            decimal longTermPositionsClosed = 0;
            decimal longTermPositionsNetProfit = 0;
            decimal shortTermPositionsClosed = 0;
            decimal shortTermPositionsNetProfit = 0;

            var closingTransactionMatches = new List<ClosingTransactionMatch>();

            foreach (var purchaseTransaction in addSellTransactionVm.MatchingPurchaseTransactions)
            {
                if (purchaseTransaction.ShareQuantity > 0)
                {
                    var purchaseTransactionDetails =
                        PurchaseTransactionRepository.GetPurchaseTransaction(purchaseTransaction.PurchaseTransactionId);

                    // Requesting to close out more shares than the position has remaining open?
                    if (purchaseTransaction.ShareQuantity > purchaseTransactionDetails.RemainingShares)
                    {
                        return -1;
                    }

                    var capitalGain = (purchaseTransaction.ShareQuantity*sellUnitPrice) // Individual Match Sell Value
                                      - (purchaseTransaction.ShareQuantity/shareQuantitySold)*sellCommission // Individual Match Sell Commission 
                                      - (purchaseTransaction.ShareQuantity*purchaseTransactionDetails.UnitPrice) // Individual Match Purchase Value
                                      - (purchaseTransaction.ShareQuantity/purchaseTransactionDetails.ShareQuantity)* purchaseTransactionDetails.Commission; // Individual Match Purchase Commission 

                    totalNetProfit += capitalGain;

                    if ((sellDate - purchaseTransactionDetails.TransactionDate).TotalDays > 365)
                    {
                        // Long Term Capital Gain - Greater than 1 year
                        longTermPositionsClosed += purchaseTransaction.ShareQuantity;
                        longTermPositionsNetProfit += capitalGain;
                    }
                    else
                    {
                        // Short Term Capital Gain - Less than 1 year
                        shortTermPositionsClosed += purchaseTransaction.ShareQuantity;
                        shortTermPositionsNetProfit += capitalGain;
                    }

                    purchaseTransactionDetails.ClosedShares += purchaseTransaction.ShareQuantity;
                    purchaseTransactionDetails.RemainingShares -= purchaseTransaction.ShareQuantity;
                    if (purchaseTransactionDetails.RemainingShares == 0)
                    {
                        purchaseTransactionDetails.PositionClosedStatus = true;
                    }

                    PurchaseTransactionRepository.UpdatePurchaseTransaction(purchaseTransactionDetails);

                    var closingTransactionMatch = new ClosingTransactionMatch
                                                      {
                                                          MatchingShareCount = purchaseTransaction.ShareQuantity,
                                                          PurchaseTransactionId = purchaseTransactionDetails.Id,
                                                          TotalNetProfit = capitalGain
                                                      };
                    closingTransactionMatches.Add(closingTransactionMatch);
                }
            }

            var sellTransaction = new SellTransaction
                                        {
                                            BrokerageAccountId = addSellTransactionVm.BrokerageAccountId,
                                            UnitPrice = sellUnitPrice,
                                            ShareQuantity = shareQuantitySold,
                                            SecurityId = addSellTransactionVm.SecurityId,
                                            Commission = sellCommission,
                                            TransactionDate = sellDate,
                                            TotalNetProfit = totalNetProfit,
                                            Profitable = (totalNetProfit >= 0),
                                            LongTermPositionsClosed = longTermPositionsClosed,
                                            LongTermPositionsNetProfit = longTermPositionsNetProfit,
                                            ShortTermPositionsClosed = shortTermPositionsClosed,
                                            ShortTermPositionsNetProfit = shortTermPositionsNetProfit,
                                            ClosingTransactionMatches = closingTransactionMatches
                                        };

            DbOperationStatus opStatus = SellTransactionRepository.InsertSellTransaction(sellTransaction);
            if (opStatus.OperationSuccessStatus)
            {
                return opStatus.AffectedIndices.First();
            }
            return -1;
        }
    }
}