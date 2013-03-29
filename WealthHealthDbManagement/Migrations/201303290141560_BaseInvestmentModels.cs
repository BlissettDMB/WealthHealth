namespace WealthHealth.DbManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseInvestmentModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brokerages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BrokerageAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        IsRetirement = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        BrokerageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Brokerages", t => t.BrokerageId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BrokerageId);
            
            CreateTable(
                "dbo.Securities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecurityId = c.Int(nullable: false),
                        BrokerageAccountId = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        ShareQuantity = c.Decimal(nullable: false, precision: 18, scale: 6),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Commission = c.Decimal(nullable: false, precision: 18, scale: 6),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Securities", t => t.SecurityId, cascadeDelete: true)
                .ForeignKey("dbo.BrokerageAccounts", t => t.BrokerageAccountId, cascadeDelete: true)
                .Index(t => t.SecurityId)
                .Index(t => t.BrokerageAccountId);
            
            CreateTable(
                "dbo.Transactions_Match",
                c => new
                    {
                        PurchaseTransactionId = c.Int(nullable: false),
                        SellTransactionId = c.Int(nullable: false),
                        MatchingShareCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TotalNetProfit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.PurchaseTransactionId, t.SellTransactionId })
                .ForeignKey("dbo.Transactions_Sell", t => t.SellTransactionId)
                .ForeignKey("dbo.Transactions_Purchase", t => t.PurchaseTransactionId)
                .Index(t => t.SellTransactionId)
                .Index(t => t.PurchaseTransactionId);
            
            CreateTable(
                "dbo.Securities_Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Securities", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Securities_MutualFunds",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Securities", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Transactions_Purchase",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ClosedShares = c.Decimal(nullable: false, precision: 18, scale: 6),
                        RemainingShares = c.Decimal(nullable: false, precision: 18, scale: 6),
                        PositionClosedStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transactions", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Transactions_Sell",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TotalNetProfit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Profitable = c.Boolean(nullable: false),
                        LongTermPositionsClosed = c.Decimal(nullable: false, precision: 18, scale: 6),
                        LongTermPositionsNetProfit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShortTermPositionsClosed = c.Decimal(nullable: false, precision: 18, scale: 6),
                        ShortTermPositionsNetProfit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transactions", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Transactions_Sell", new[] { "Id" });
            DropIndex("dbo.Transactions_Purchase", new[] { "Id" });
            DropIndex("dbo.Securities_MutualFunds", new[] { "Id" });
            DropIndex("dbo.Securities_Stocks", new[] { "Id" });
            DropIndex("dbo.Transactions_Match", new[] { "PurchaseTransactionId" });
            DropIndex("dbo.Transactions_Match", new[] { "SellTransactionId" });
            DropIndex("dbo.Transactions", new[] { "BrokerageAccountId" });
            DropIndex("dbo.Transactions", new[] { "SecurityId" });
            DropIndex("dbo.BrokerageAccounts", new[] { "BrokerageId" });
            DropIndex("dbo.BrokerageAccounts", new[] { "UserId" });
            DropForeignKey("dbo.Transactions_Sell", "Id", "dbo.Transactions");
            DropForeignKey("dbo.Transactions_Purchase", "Id", "dbo.Transactions");
            DropForeignKey("dbo.Securities_MutualFunds", "Id", "dbo.Securities");
            DropForeignKey("dbo.Securities_Stocks", "Id", "dbo.Securities");
            DropForeignKey("dbo.Transactions_Match", "PurchaseTransactionId", "dbo.Transactions_Purchase");
            DropForeignKey("dbo.Transactions_Match", "SellTransactionId", "dbo.Transactions_Sell");
            DropForeignKey("dbo.Transactions", "BrokerageAccountId", "dbo.BrokerageAccounts");
            DropForeignKey("dbo.Transactions", "SecurityId", "dbo.Securities");
            DropForeignKey("dbo.BrokerageAccounts", "BrokerageId", "dbo.Brokerages");
            DropForeignKey("dbo.BrokerageAccounts", "UserId", "dbo.Users");
            DropTable("dbo.Transactions_Sell");
            DropTable("dbo.Transactions_Purchase");
            DropTable("dbo.Securities_MutualFunds");
            DropTable("dbo.Securities_Stocks");
            DropTable("dbo.Transactions_Match");
            DropTable("dbo.Transactions");
            DropTable("dbo.Securities");
            DropTable("dbo.BrokerageAccounts");
            DropTable("dbo.Brokerages");
        }
    }
}
