using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.PoolDemo.IServices;
using Ray.EssayNotes.Di.PoolDemo.Services;
using Ray.Infrastructure.Extensions.MsDi;

namespace Ray.EssayNotes.Di.PoolDemo.Test
{
    [Description("容器中的【持久化实例池】")]
    public class Test02 : TestBase
    {
        public override void Run()
        {
            Console.WriteLine("\r\n【根域】");
            Program.ServiceProviderRoot.GetService<IMyTransientService>();
            Program.ServiceProviderRoot.GetService<IMySingletonService>();
            Program.ServiceProviderRoot.GetService<IMyScopedService>();
            Program.ServiceProviderRoot.GetService<TestSingletonService>();

            var resolvedServices = Program.ServiceProviderRoot.GetResolvedServicesFromScope()
                .ToDictionary(x => x.Key.Type.Name, x => x.Value);
            Console.WriteLine($"持久化实例池：{resolvedServices.AsFormatJsonStr(false)}");

            using (var childScope = Program.ServiceProviderRoot.CreateScope())
            {
                Console.WriteLine("\r\n【子域】");

                var childServiceProvider = childScope.ServiceProvider;
                childServiceProvider.GetService<IMyTransientService>();
                childServiceProvider.GetService<IMySingletonService>();
                childServiceProvider.GetService<IMyScopedService>();
                childServiceProvider.GetService<TestSingletonService>();

                var childResoledServices = childServiceProvider.GetResolvedServicesFromScope()
                    .ToDictionary(x => x.Key.Type.Name, x => x.Value); ;
                Console.WriteLine($"持久化实例池：{childResoledServices.AsFormatJsonStr(false)}");
            }
        }

    }
}
