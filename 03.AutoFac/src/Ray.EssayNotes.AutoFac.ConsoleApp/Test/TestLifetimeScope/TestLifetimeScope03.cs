using System;
using System.ComponentModel;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions;
using Ray.EssayNotes.AutoFac.Service.Dtos;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    [Description("全局单例")]
    public class TestLifetimeScope03 : TestLifetimeScopeBase
    {
        /// <summary>
        /// 全局单例
        /// 【SingleInstance】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.RegisterType<DtoToken>()
                .SingleInstance();

            return builder;
        }

        protected override void PrintResult()
        {
            //直接从根容器取
            var instance1 = RayContainer.AutofacRootScope.Resolve<DtoToken>();
            Console.WriteLine($"第1次：{instance1.Guid}");

            var instance2 = RayContainer.AutofacRootScope.Resolve<DtoToken>();
            Console.WriteLine($"第2次：{instance2.Guid}");

            using (var scope = RayContainer.AutofacRootScope.BeginLifetimeScope())
            {
                var instance3 = scope.Resolve<DtoToken>();
                Console.WriteLine($"第3次：{instance3.Guid}");

                var instance4 = scope.Resolve<DtoToken>();
                Console.WriteLine($"第4次：{instance4.Guid}");
            }
        }
    }
}
