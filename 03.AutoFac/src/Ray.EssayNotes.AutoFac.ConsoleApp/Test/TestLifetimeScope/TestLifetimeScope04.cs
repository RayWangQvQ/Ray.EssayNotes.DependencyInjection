using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions;
using Ray.EssayNotes.AutoFac.Service.Dtos;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    public class TestLifetimeScope04 : TestLifetimeScopeBase
    {
        /// <summary>
        /// 域内单例
        /// 【InstancePerLifetimeScope】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.RegisterType<DtoToken>()
                .InstancePerLifetimeScope();

            return builder;
        }

        protected override void PrintResult()
        {
            var instance1 = MyContainer.Root.Resolve<DtoToken>();
            Console.WriteLine($"第1次：{instance1.Guid}");

            var instance2 = MyContainer.Root.Resolve<DtoToken>();
            Console.WriteLine($"第2次：{instance2.Guid}");

            using (var scope = MyContainer.Root.BeginLifetimeScope())
            {
                var instance3 = scope.Resolve<DtoToken>();
                Console.WriteLine($"第3次：{instance3.Guid}");

                var instance4 = scope.Resolve<DtoToken>();
                Console.WriteLine($"第4次：{instance4.Guid}");
            }

            using (var scope = MyContainer.Root.BeginLifetimeScope())
            {
                var instance5 = scope.Resolve<DtoToken>();
                Console.WriteLine($"第5次：{instance5.Guid}");

                using (var scope2 = MyContainer.Root.BeginLifetimeScope())
                {
                    var instance6 = scope2.Resolve<DtoToken>();
                    Console.WriteLine($"第6次：{instance6.Guid}");

                    var instance7 = scope2.Resolve<DtoToken>();
                    Console.WriteLine($"第7次：{instance7.Guid}");
                }
            }
        }
    }
}
