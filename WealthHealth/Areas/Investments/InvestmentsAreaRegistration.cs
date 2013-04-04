using System.Web.Mvc;

namespace WealthHealth.Areas.Investments
{
    public class InvestmentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Investments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Investments_default",
                "Investments/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}