//系统包
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
//本地项目包
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;

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


            //1.初始化容器，注册组件
            MyContainer.Init(builder =>
            {
                new Startup().ConfigureServices(builder);
                return builder;
            });
            //AutoFac解析器
            System.Web.Mvc.IDependencyResolver autoFacResolver = new Autofac.Integration.Mvc.AutofacDependencyResolver(MyContainer.Instance);
            //将AutoFac解析器设置为系统DI解析器
            DependencyResolver.SetResolver(autoFacResolver);


        }
    }
}
