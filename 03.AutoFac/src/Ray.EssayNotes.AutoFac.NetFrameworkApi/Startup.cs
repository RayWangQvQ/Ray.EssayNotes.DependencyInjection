using System.Reflection;
using Autofac.Integration.WebApi;
using Ray.EssayNotes.AutoFac.Service.Di;

namespace Ray.EssayNotes.AutoFac.NetFrameworkApi
{
    public class Startup
    {
        public void ConfigureServices(Autofac.ContainerBuilder builder)
        {
            var mvcAssembly = Assembly.GetExecutingAssembly();

            builder.AddRepositories()
                .AddAppServices()
                .RegisterApiControllers(mvcAssembly);
        }
    }
}
