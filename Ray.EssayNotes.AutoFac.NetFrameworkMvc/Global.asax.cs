using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ray.EssayNotes.AutoFac.NetFrameworkMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcContainer.Init();
        }
    }
}