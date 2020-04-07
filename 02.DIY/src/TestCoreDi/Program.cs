using ApplicationService.IServices;
using ApplicationService.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace TestCoreDi
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            //TestValidateScopes();
            //TestValidateOnBuild();
            //TestInjectServiceProvider();

            Console.ReadKey();
        }

        private static void Test()
        {
            ServiceProvider root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();
            var provider1 = root.CreateScope().ServiceProvider;
            var provider2 = root.CreateScope().ServiceProvider;

            void GetServices<T>(IServiceProvider provider)
            {
                provider.GetService<T>();
                provider.GetService<T>();
            }

            GetServices<IFoo>(provider1);
            GetServices<IBar>(provider1);
            GetServices<IBaz>(provider1);
            Console.WriteLine();
            GetServices<IFoo>(provider2);
            GetServices<IBar>(provider2);
            GetServices<IBaz>(provider2);
        }

        /// <summary>
        /// 测试范围验证
        /// </summary>
        private static void TestValidateScopes()
        {
            void TryResolveService(bool validateScopes)
            {
                var root = new ServiceCollection()
                    .AddSingleton<IFoo, Foo>()
                    .AddScoped<IBar, Bar>()
                    .BuildServiceProvider(validateScopes);//是否允许提供以单例形式存在的Scoped服务
                var child = root.CreateScope().ServiceProvider;

                void ResolveService<T>(IServiceProvider provider)
                {
                    var isRootContainer = root == provider ? "Yes" : "No";
                    try
                    {
                        provider.GetService<T>();
                        Console.WriteLine($@"Status: Success; Service Type: { typeof(T).Name}; Root: { isRootContainer}");
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($@"Status: Fail; Service Type: { typeof(T).Name}; Root: { isRootContainer}");
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine();
                    }
                }

                ResolveService<IFoo>(root);
                ResolveService<IBar>(root);
                ResolveService<IFoo>(child);
                ResolveService<IBar>(child);
            }
            Console.WriteLine("不验证：");
            TryResolveService(false);
            Console.WriteLine("验证：");
            TryResolveService(true);
        }

        /// <summary>
        /// 注册时验证服务是否可构造
        /// </summary>
        private static void TestValidateOnBuild()
        {
            static void BuildServiceProvider(bool validateOnBuild)
            {
                try
                {
                    var options = new ServiceProviderOptions
                    {
                        ValidateOnBuild = validateOnBuild
                    };
                    new ServiceCollection()
                        .AddSingleton<IFoobar, Foobar>()
                        .BuildServiceProvider(options);
                    Console.WriteLine($"Status: Success; ValidateOnBuild: {validateOnBuild}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Status: Fail; ValidateOnBuild: {validateOnBuild}");
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("不验证：");
            BuildServiceProvider(false);
            Console.WriteLine("验证：");
            BuildServiceProvider(true);
        }

        /// <summary>
        /// 测试注入容器
        /// </summary>
        private static void TestInjectServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<SingletonService>()
                .AddScoped<ScopedService>()
                .BuildServiceProvider();

            (Type engineType, Type engineScopeType) = ResolveTypes();
            object engine = GetEngine(serviceProvider);
            IServiceScope rootScope = GetRootScope(engine, engineType);

            using (var scope = serviceProvider.CreateScope())
            {
                var child = scope.ServiceProvider;
                var singletonService = child.GetRequiredService<SingletonService>();
                var scopedService = child.GetRequiredService<ScopedService>();

                Console.WriteLine(ReferenceEquals(child, child.GetRequiredService<IServiceProvider>()));
                Console.WriteLine(ReferenceEquals(child, scopedService.RequestServices));
                Console.WriteLine(ReferenceEquals(child, singletonService.ApplicationServices));
                Console.WriteLine(ReferenceEquals(serviceProvider, singletonService.ApplicationServices));
                Console.WriteLine(ReferenceEquals(rootScope, singletonService.ApplicationServices));
            }

            static (Type Engine, Type EngineScope) ResolveTypes()
            {
                var assembly = typeof(ServiceProvider).Assembly;
                var engine = assembly.GetTypes().Single(it => it.Name == "IServiceProviderEngine");
                var engineScope = assembly.GetTypes().Single(it => it.Name == "ServiceProviderEngineScope");
                return (engine, engineScope);
            }

            static object GetEngine(ServiceProvider serviceProvider)
            {
                var field = typeof(ServiceProvider).GetField("_engine", BindingFlags.Instance | BindingFlags.NonPublic);
                return field.GetValue(serviceProvider);
            }

            static IServiceScope GetRootScope(object engine, Type engineType)
            {
                var property = engineType.GetProperty("RootScope", BindingFlags.Instance | BindingFlags.Public);
                return (IServiceScope)property.GetValue(engine);
            }
        }
    }

    public class SingletonService
    {
        public IServiceProvider ApplicationServices { get; }
        public SingletonService(IServiceProvider serviceProvider) => ApplicationServices = serviceProvider;
    }

    public class ScopedService
    {
        public IServiceProvider RequestServices { get; }
        public ScopedService(IServiceProvider serviceProvider) => RequestServices = serviceProvider;
    }
}
