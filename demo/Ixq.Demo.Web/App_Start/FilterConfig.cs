using System.Web.Mvc;
using Ixq.Web.Mvc;

namespace Ixq.Demo.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new RuntimeLogHandleErrorAttribute());
        }
    }
}