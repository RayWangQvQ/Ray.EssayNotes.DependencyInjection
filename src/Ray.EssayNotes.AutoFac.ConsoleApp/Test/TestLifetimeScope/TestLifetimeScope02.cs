using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.AppService;
using Ray.EssayNotes.AutoFac.Service.Dtos;
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
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
                .AsSelf()
                .InstancePerDependency();//如果不指定生命周期，这也是默认的选项。

            return builder;
        }

        protected override void PrintResult()
        {
            //打印注册信息
            Console.WriteLine(ConsoleContainer.Instance.PrintComponent<DtoToken>());

            using (var scope = ConsoleContainer.Instance.BeginLifetimeScope())
            {
                for (int i = 0; i < 3; i++)
                {
                    var instance = scope.Resolve<DtoToken>();//其实就像当与每次都new一下，且new完之后，不会将实例持久化到容器中

                    Console.WriteLine($"第{i + 1}次：{instance.Guid}");
                }
            }
        }
    }
}
