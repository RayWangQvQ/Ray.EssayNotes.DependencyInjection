using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.PoolDemo.IServices;

namespace Ray.EssayNotes.Di.PoolDemo.Test
{
    [Description("容器中的【可释放实例池】")]
    public class Test03 : TestBase
    {
        public override void Run()
        {
            Console.WriteLine("\r\n【根域】");
            Program.ServiceProviderRoot.GetService<IMyTransientService>();
            Program.ServiceProviderRoot.GetService<IMySingletonService>();
            Program.ServiceProviderRoot.GetService<IMyScopedService>();

            var instances = Program.ServiceProviderRoot.GetDisposablesFromScope();
            Console.WriteLine("可释放实例池：");
            foreach (var obj in instances)
            {
                Console.WriteLine(obj);
            }

            using (var childScope = Program.ServiceProviderRoot.CreateScope())
            {
                Console.WriteLine("\r\n【子域】");

                var childServiceProvider = childScope.ServiceProvider;
                childServiceProvider.GetService<IMyTransientService>();
                childServiceProvider.GetService<IMySingletonService>();
                childServiceProvider.GetService<IMyScopedService>();

                var childResoledServices = childServiceProvider.GetDisposablesFromScope();
                Console.WriteLine("n可释放实例池：");
                foreach (var obj in childResoledServices)
                {
                    Console.WriteLine(obj);
                }
            }
        }

    }
}
