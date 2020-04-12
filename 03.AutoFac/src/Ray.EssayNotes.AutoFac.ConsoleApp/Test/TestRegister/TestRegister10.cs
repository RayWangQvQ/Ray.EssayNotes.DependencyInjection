using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using System.Collections.Generic;
using System.ComponentModel;
using Autofac.Core;
using System.Reflection;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    [Description("命名实例注入")]
    public class TestRegister10 : TestRegisterBase
    {
        protected override void PrintResult()
        {
            var service = MyContainer.Root.Resolve<IMyService>();
            Console.WriteLine($"{service.GetHashCode()}");

            var other = MyContainer.Root.ResolveNamed<IMyService>("other");
            Console.WriteLine($"{other.GetHashCode()}");

            var services = MyContainer.Root.Resolve<IEnumerable<IMyService>>();
            foreach (var item in services)
            {
                Console.WriteLine($"{item.GetHashCode()}");
            }

        }

        /// <summary>
        /// 命名实例注册
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<MyService>()
                .As<IMyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MyOtherService>()
                .Named<IMyService>("other")
                .InstancePerLifetimeScope();
            return builder;
        }
    }
}
