//系统包
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Ray.EssayNotes.AutoFac.CoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                //第一种：使用自带DI
                //.UseStartup<Startup>();

                //第二种：添加AutoFac作为辅助容器
                //.HookAutoFacIntoPipeline()
                //.UseStartup<StartupWithAutoFac>();

                //第三种：添加AutoFac接管依赖注入
                webBuilder.UseStartup<StartupOnlyAutoFac>();
            });

    }
}
