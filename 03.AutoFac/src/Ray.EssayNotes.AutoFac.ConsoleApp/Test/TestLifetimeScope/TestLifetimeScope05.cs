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
            Print01();
            Print02();
            Print03();
            Print04();
            Print05();
            Print06();
            Print07();
        }

        /// <summary>
        /// 直接从指定的标签域内解析
        /// </summary>
        private void Print01()
        {
            Console.WriteLine("testScopeName标签域内：");

            using (var testScope = MyContainer.Root.BeginLifetimeScope("testScopeName"))
            {
                for (int i = 1; i < 3; i++)
                {
                    var instance = testScope.Resolve<DtoToken>();
                    Console.WriteLine($"第{i}次：{instance.Guid}");
                }
            }
        }

        /// <summary>
        /// 从根域内解析
        /// </summary>
        private void Print02()
        {
            Console.WriteLine("根域内：");

            try
            {
                for (int i = 1; i < 3; i++)
                {
                    var instance = MyContainer.Root.ResolveOptional<DtoToken>();
                    Console.WriteLine($"第{i}次：{instance?.Guid}");
                }
            }
            catch
            {
                Console.WriteLine("异常");
            }
        }

        /// <summary>
        /// 直接从无标签域
        /// </summary>
        private void Print03()
        {
            Console.WriteLine("无标签域内：");
            try
            {
                using (var testScope = MyContainer.Root.BeginLifetimeScope())
                {
                    for (int i = 1; i < 3; i++)
                    {
                        var instance = testScope.Resolve<DtoToken>();
                        Console.WriteLine($"第{i}次：{instance.Guid}");
                    }
                }
            }
            catch
            {
                Console.WriteLine("异常");
            }
        }

        /// <summary>
        /// 直接从其他标签域
        /// </summary>
        private void Print04()
        {
            Console.WriteLine("其他标签域内：");
            try
            {
                using (var testScope = MyContainer.Root.BeginLifetimeScope("abc"))
                {
                    for (int i = 1; i < 3; i++)
                    {
                        var instance = testScope.Resolve<DtoToken>();
                        Console.WriteLine($"第{i}次：{instance.Guid}");
                    }
                }
            }
            catch
            {
                Console.WriteLine("异常");
            }
        }

        /// <summary>
        /// 从指定标签域下的无标签域
        /// </summary>
        private void Print05()
        {
            Console.WriteLine("testScopeName.空：");
            try
            {
                using (var testScope = MyContainer.Root.BeginLifetimeScope("testScopeName"))
                {
                    using (var scope = testScope.BeginLifetimeScope())
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            var instance = scope.Resolve<DtoToken>();
                            Console.WriteLine($"第{i}次：{instance.Guid}");
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("异常");
            }
        }

        /// <summary>
        /// 从指定标签域下的其他标签域
        /// </summary>
        private void Print06()
        {
            Console.WriteLine("testScopeName.aaa：");
            try
            {
                using (var testScope = MyContainer.Root.BeginLifetimeScope("testScopeName"))
                {
                    using (var scope = testScope.BeginLifetimeScope("aaa"))
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            var instance = scope.Resolve<DtoToken>();
                            Console.WriteLine($"第{i}次：{instance.Guid}");
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("异常");
            }
        }

        /// <summary>
        /// 从指定标签域下的其他标签域下的其他标签域
        /// </summary>
        private void Print07()
        {
            Console.WriteLine("testScopeName.aaa.bbb：");
            try
            {
                using (var testScope = MyContainer.Root.BeginLifetimeScope("testScopeName"))
                {
                    using (var scopeA = testScope.BeginLifetimeScope("aaa"))
                    {
                        using (var scopeB = scopeA.BeginLifetimeScope("bbb"))
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                var instance = scopeB.Resolve<DtoToken>();
                                Console.WriteLine($"第{i}次：{instance.Guid}");
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("异常");
            }
        }
    }
}
