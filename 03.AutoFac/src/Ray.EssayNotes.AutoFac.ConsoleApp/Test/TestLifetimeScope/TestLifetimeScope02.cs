using System;
using System.ComponentModel;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.Dtos;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    [Description("瞬时实例（每次依赖一个实例）")]
    public class TestLifetimeScope02 : TestLifetimeScopeBase
    {
        /// <summary>
        /// 瞬时实例（每次依赖一个实例）
        /// 【InstancePerDependency】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.RegisterType<DtoToken>()
                .InstancePerDependency();//如果不指定生命周期，这也是默认的选项

            return builder;
        }

        protected override void PrintResult()
        {
            using (var scope = MyContainer.Root.BeginLifetimeScope())
            {
                for (int i = 0; i < 3; i++)
                {
                    var instance = scope.Resolve<DtoToken>();//其实就相当于每次都new一下，且new完之后，不会将实例持久化到容器中

                    Console.WriteLine($"第{i + 1}次：{instance.Guid}");
                }
            }
        }
    }
}
