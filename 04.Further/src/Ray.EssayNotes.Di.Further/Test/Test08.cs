using System;
using System.Text.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.Further.Extensions;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Ray.EssayNotes.Di.ContainerDemo.IServices;
using Ray.EssayNotes.Di.ContainerDemo.Services;

namespace Ray.EssayNotes.Di.Further.Test
{
    [Description("向待解析对象中注入容器")]
    public class Test08 : TestBase
    {
        public override void ReBuildContainer()
        {
            Program.ServiceProviderRoot = new ServiceCollection()
                .AddSingleton<SingletonService>()
                .AddScoped<ScopedService>()
                .AddTransient<TransientService>()
                .BuildServiceProvider();
        }

        public override void Print()
        {
            using (var childScope = Program.ServiceProviderRoot.CreateScope())
            {
                var childServiceProvider = childScope.ServiceProvider;
                var singletonService = childServiceProvider.GetRequiredService<SingletonService>();
                var scopedService = childServiceProvider.GetRequiredService<ScopedService>();
                var transientService = childServiceProvider.GetRequiredService<TransientService>();

                Console.WriteLine($"根容器（ServiceProvider对象）:{Program.ServiceProviderRoot.GetHashCode()}");
                Console.WriteLine($"根域：{Program.ServiceProviderRoot.GetRequiredService<IServiceProvider>().GetHashCode()}");

                Console.WriteLine($"子容器：{childServiceProvider.GetHashCode()}");
                Console.WriteLine($"子域：{childScope.GetHashCode()}");

                Console.WriteLine($"\r\n单例对象中注入的容器：{singletonService.ApplicationServices.GetHashCode()}");
                Console.WriteLine($"域内单例对象注入的容器：{scopedService.RequestServices.GetHashCode()}");
                Console.WriteLine($"瞬时实例对象注入的容器：{transientService.RequestServices.GetHashCode()}");
            }
        }


        public class SingletonService
        {
            public IServiceProvider ApplicationServices { get; }
            public SingletonService(IServiceProvider serviceProvider) => ApplicationServices = serviceProvider;
        }

        public class ScopedService
        {
            public IServiceProvider RequestServices { get; }
            public ScopedService(IServiceProvider serviceProvider) => RequestServices = serviceProvider;
        }

        public class TransientService
        {
            public IServiceProvider RequestServices { get; }
            public TransientService(IServiceProvider serviceProvider) => RequestServices = serviceProvider;
        }
    }
}
