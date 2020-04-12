using System;
using System.ComponentModel;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions;
using Ray.EssayNotes.AutoFac.Service.Dtos;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    [Description("指定标签域内单例")]
    public class TestLifetimeScope05 : TestLifetimeScopeBase
    {
        /// <summary>
        /// 指定标签域内单例
        /// 【InstancePerMatchingLifetimeScope】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.RegisterType<DtoToken>()
                .InstancePerMatchingLifetimeScope("testScopeName");//表明：只能在名称为testScopeName的域内（包括多级子域）被解析，并且在该域内单例

            return builder;
        }

        protected override void PrintResult()
        {
            using (var testScope = MyContainer.Root.BeginLifetimeScope("testScopeName"))
            {
                var instance1 = testScope.Resolve<DtoToken>();//实例化，并将实例持久化到该域内
                Console.WriteLine($"【testScopeName】第1次：{instance1.Guid}");

                var instance2 = testScope.Resolve<DtoToken>();//直接从域内获取持久化的实例
                Console.WriteLine($"【testScopeName】第2次：{instance2.Guid}");

                using (var defScope = testScope.BeginLifetimeScope())
                {
                    var instance3 = defScope.Resolve<DtoToken>();
                    Console.WriteLine($"第3次：{instance3.Guid}");
                }

                using (var defScope = testScope.BeginLifetimeScope("abc"))
                {
                    var instance3 = defScope.Resolve<DtoToken>();
                    Console.WriteLine($"【abc】第4次：{instance3.Guid}");

                }
            }

            using (var abcScope = MyContainer.Root.BeginLifetimeScope())
            {
                try
                {
                    var instance1 = abcScope.Resolve<DtoToken>();
                    Console.WriteLine($"【无标签】第5次：{instance1.Guid}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"【无标签】第5次：{ex.Message}");
                }
            }
        }
    }
}
