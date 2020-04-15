using System;
using System.ComponentModel;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions;
using Ray.EssayNotes.AutoFac.Service.Dtos;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    [Description("请求域内单例")]
    public class TestLifetimeScope06 : TestLifetimeScopeBase
    {
        /// <summary>
        /// 请求域内单例
        /// 【InstancePerRequest】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.RegisterType<DtoToken>()
                .InstancePerRequest();
            //.InstancePerMatchingLifetimeScope(Autofac.Core.Lifetime.MatchingScopeLifetimeTags.RequestLifetimeScopeTag)
            //.InstancePerMatchingLifetimeScope("AutofacWebRequest");

            return builder;
        }

        protected override void PrintResult()
        {
            using (var testScope = MyContainer.Root.BeginLifetimeScope("AutofacWebRequest"))
            {
                var instance1 = testScope.Resolve<DtoToken>();
                Console.WriteLine($"【AutofacWebRequest】第1次：{instance1.Guid}");

                var instance2 = testScope.Resolve<DtoToken>();
                Console.WriteLine($"【AutofacWebRequest】第2次：{instance2.Guid}");

                using (var defScope = testScope.BeginLifetimeScope("abc"))
                {
                    try
                    {
                        var instance3 = defScope.Resolve<DtoToken>();
                        Console.WriteLine($"【AutofacWebRequest.abc】第3次：{instance3.Guid}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"【AutofacWebRequest.abc】第3次：{ex.Message}");
                    }
                }
            }

            using (var abcScope = MyContainer.Root.BeginLifetimeScope())
            {
                try
                {
                    var instance1 = abcScope.Resolve<DtoToken>();
                    Console.WriteLine($"【无标签】第4次：{instance1.Guid}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"【无标签】第4次：{ex.Message}");
                }
            }
        }
    }
}
