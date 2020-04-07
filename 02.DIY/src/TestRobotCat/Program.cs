using System;
using System.Reflection;
using ApplicationService.IServices;
using ApplicationService.Services;
using Ray.DependencyInjection;
using Ray.DependencyInjection.Enums;
using Ray.DependencyInjection.Extensions;

namespace TestRobotCat
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new RobotCat()
                .Register<IFoo, Foo>(LifetimeEnum.Transient)
                .Register<IBar>(_ => new Bar(), LifetimeEnum.Self)
                .Register<IBaz, Baz>(LifetimeEnum.Root)
                .Register(Assembly.GetEntryAssembly());//注册标注了MapToAttribute特性的服务
            var cat1 = root.CreateChild();
            var cat2 = root.CreateChild();

            void GetServices<TService>(RobotCat cat)
            {
                cat.GetService<TService>();
                cat.GetService<TService>();
            }

            GetServices<IFoo>(cat1);
            GetServices<IBar>(cat1);
            GetServices<IBaz>(cat1);
            GetServices<IGux>(cat1);
            Console.WriteLine();
            GetServices<IFoo>(cat2);
            GetServices<IBar>(cat2);
            GetServices<IBaz>(cat2);
            GetServices<IGux>(cat2);

            Console.ReadKey();
        }
    }
}
