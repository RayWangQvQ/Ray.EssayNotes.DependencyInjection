using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Ray.EssayNotes.Di.StartupDemo.Test
{
    public class Test01
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults：指定服务相关配置");
                    webBuilder.UseStartup<Startup>();
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
