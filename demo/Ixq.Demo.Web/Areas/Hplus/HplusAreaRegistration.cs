using System.Web.Mvc;

namespace Ixq.Demo.Web.Areas.Hplus
{
    public class HplusAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Hplus"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            var route = context.MapRoute(
                "Hplus_default",
                "Hplus/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                new[] {"Ixq.Demo.Web.Areas.Hplus.Controllers"}
                );

            //route.DataTokens["authorizeRole"] = "";
            //route.DataTokens["loginUrl"] = "/Hplus/Account/Login";
        }
    }
}