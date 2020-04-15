using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ray.EssayNotes.Di.StartupDemo.Test
{
    /// <summary>
    /// Startup类不是必要的
    /// </summary>
    public class Test03
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults：指定服务相关配置");
                    webBuilder.ConfigureServices(services =>
                    {
                        Console.WriteLine("进入Startup.ConfigureServices，注册组件到容器");
                        services.AddControllers();
                    });
                    webBuilder.Configure(app =>
                    {
                        Console.WriteLine("进入Startup.Configure，配置管道");

                        app.UseHttpsRedirection();

                        app.UseRouting();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                })
                .ConfigureHostConfiguration(cof =>
                {
                    Console.WriteLine("ConfigureHostConfiguration：指定构建器自身配置");
                })
                .ConfigureAppConfiguration(cof =>
                {
                    Console.WriteLine("ConfigureAppConfiguration：其他配置");
                })
                .ConfigureServices(cof =>
                {
                    Console.WriteLine("ConfigureServices：注册组件到容器");
                });
    }
}
