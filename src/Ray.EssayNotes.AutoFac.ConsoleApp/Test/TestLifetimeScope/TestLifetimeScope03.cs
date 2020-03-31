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
    public class TestLifetimeScope03 : TestLifetimeScopeBase
    {
        /// <summary>
        /// 指定域内单例
        /// 【InstancePerMatchingLifetimeScope】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.RegisterType<DtoToken>()
                .AsSelf()
                .InstancePerMatchingLifetimeScope("testScopeName");//表明：只能在名称为testScopeName的域内被解析，并且在该域内单例

            return builder;
        }

        /// <summary>
        /// 从根容器创建一个子域
        /// </summary>
        protected override void PrintResult()
        {
            //打印注册信息
            Console.WriteLine(ConsoleContainer.Instance.PrintComponent<DtoToken>());

            using (var testScope = ConsoleContainer.Instance.BeginLifetimeScope("testScopeName"))
            {
                var instance1 = testScope.Resolve<DtoToken>();//实例化，并将实例持久化到该域内
                Console.WriteLine($"【testScopeName】第1次：{instance1.Guid}");

                var instance2 = testScope.Resolve<DtoToken>();//直接从域内获取持久化的实例
                Console.WriteLine($"【testScopeName】第2次：{instance2.Guid}");

                using (var defScope = ConsoleContainer.Instance.BeginLifetimeScope("abc"))
                {
                    try
                    {
                        var instance3 = defScope.Resolve<DtoToken>();
                        Console.WriteLine($"【testScopeName.abc】第3次：{instance3.Guid}");
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            using (var abcScope = ConsoleContainer.Instance.BeginLifetimeScope("def"))
            {
                try
                {
                    var instance1 = abcScope.Resolve<DtoToken>();
                    Console.WriteLine($"【def】：{instance1.Guid}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"【def】：{ex.Message}");
                }
            }
        }
    }
}
