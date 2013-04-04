using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using WealthHealth.App_Start;
using WealthHealth.DbManagement;
using WealthHealth.Repositories.Investments;
using WealthHealth.Services.DependencyResolution;
using WealthHealth.Areas.Investments.Services.Investments.BrokerageAccounts;
using WealthHealth.Areas.Investments.Services.Investments.Securities;
using WealthHealth.Areas.Investments.Services.Investments.Transactions;
using WebMatrix.WebData;

namespace WealthHealth
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebSecurity.InitializeDatabaseConnection("WealthHealthDB", "Users", "Id", "UserName", autoCreateTables: true);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            GlobalConfig.CustomizeConfig(GlobalConfiguration.Configuration);

            RegisterDependencyResolver();
        }

        private void RegisterDependencyResolver()
        {
            //Initialize Unity Container
            var container = new UnityContainer();

            //Register Repositories and Default DbContext
            container.RegisterType<DbContext, WealthHealthDB>();
            container.RegisterType<IBrokerageAccountRepository, BrokerageAccountRepository<WealthHealthDB>>();
            container.RegisterType<IStockRepository, StockRepository<WealthHealthDB>>();
            container.RegisterType<IMutualFundRepository, MutualFundRepository<WealthHealthDB>>();
            container.RegisterType<IPurchaseTransactionRepository, PurchaseTransactionRepository<WealthHealthDB>>();
            container.RegisterType<ISellTransactionRepository, SellTransactionRepository<WealthHealthDB>>();

            //Register Services
            container.RegisterType<IBrokerageAccountService, BrokerageAccountService>();
            container.RegisterType<IStockService, StockService>();
            container.RegisterType<IMutualFundService, MutualFundService>();
            container.RegisterType<IPurchaseTransactionService, PurchaseTransactionService>();
            container.RegisterType<ISellTransactionService, SellTransactionService>();

            //IoC
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}