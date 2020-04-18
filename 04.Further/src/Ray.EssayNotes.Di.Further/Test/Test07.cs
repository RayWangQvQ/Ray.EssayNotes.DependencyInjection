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

namespace Ray.EssayNotes.Di.Further.Test
{
    [Description("总结")]
    public class Test07 : TestBase
    {
        public override void Print()
        {
            if (Program.ChildScope1 == null) Program.ChildScope1 = Program.ServiceProviderRoot.CreateScope();
            if (Program.ChildScope2 == null) Program.ChildScope2 = Program.ServiceProviderRoot.CreateScope();

            Console.WriteLine("\r\n引擎(ServiceProviderEngine)的唯一性：");
            Console.WriteLine($"根容器下的引擎：{Program.ServiceProviderRoot.GetEngine().GetHashCode()}");
            Console.WriteLine($"1号子容器/域下的引擎：{Program.ChildScope1.ServiceProvider.GetEngine().GetHashCode()}");
            Console.WriteLine($"2号子容器/域下的引擎：{Program.ChildScope2.ServiceProvider.GetEngine().GetHashCode()}");

            Console.WriteLine("\r\n引擎(ServiceProviderEngine)和工厂(IServiceScopeFactory)的同一性：");
            Console.WriteLine($"根容器下的工厂：{Program.ServiceProviderRoot.GetRequiredService<IServiceScopeFactory>().GetHashCode()}");
            Console.WriteLine($"1号子容器/域下的工厂：{Program.ChildScope1.ServiceProvider.GetRequiredService<IServiceScopeFactory>().GetHashCode()}");
            Console.WriteLine($"2号子容器/域下的工厂：{Program.ChildScope2.ServiceProvider.GetRequiredService<IServiceScopeFactory>().GetHashCode()}");

            Console.WriteLine("\r\n引擎域(ServiceProviderEngineScope)和域所包裹容器（IServiceProvider）的同一性");
            IServiceScope rootScope = (IServiceScope)Program.ServiceProviderRoot.GetEngine().GetPropertyValue("RootScope");
            Console.WriteLine($"根引擎域：{rootScope.GetHashCode()};根域包裹的容器：{rootScope.ServiceProvider.GetHashCode()}");
            Console.WriteLine($"1号引擎域：{Program.ChildScope1.GetHashCode()};1号域包裹的容器：{Program.ChildScope1.ServiceProvider.GetHashCode()}");
            Console.WriteLine($"2号引擎域：{Program.ChildScope2.GetHashCode()};2号域包裹的容器：{Program.ChildScope2.ServiceProvider.GetHashCode()}");

            Console.WriteLine("\r\n从引擎域(ServiceProviderEngineScope)解析IServiceProvider，得到的是自己");
            Console.WriteLine($"根域：{rootScope.GetHashCode()}；从根域解析：{rootScope.ServiceProvider.GetRequiredService<IServiceProvider>().GetHashCode()}");
            Console.WriteLine($"1号子域：{Program.ChildScope1.GetHashCode()}；从1号子域解析：{Program.ChildScope1.ServiceProvider.GetRequiredService<IServiceProvider>().GetHashCode()}");
            Console.WriteLine($"2号子域：{Program.ChildScope2.GetHashCode()}；从2号子域解析：{Program.ChildScope2.ServiceProvider.GetRequiredService<IServiceProvider>().GetHashCode()}");

            Console.WriteLine("\r\n从ServiceProvider解析IServiceProvider，得到的不是自己，是根引擎域");
            Console.WriteLine($"ServiceProvider：{Program.ServiceProviderRoot.GetHashCode()}；从ServiceProvider解析：{Program.ServiceProviderRoot.GetRequiredService<IServiceProvider>().GetHashCode()}");

        }
    }
}
