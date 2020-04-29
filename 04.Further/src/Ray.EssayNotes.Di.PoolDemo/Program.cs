using System;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.PoolDemo.IServices;
using Ray.EssayNotes.Di.PoolDemo.Services;
using Ray.EssayNotes.Di.PoolDemo.Test;

namespace Ray.EssayNotes.Di.PoolDemo
{
    class Program
    {
        /// <summary>
        /// 根容器
        /// </summary>
        public static IServiceProvider ServiceProviderRoot { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TestSingletonService testSingletonService = new TestSingletonService();

            Program.ServiceProviderRoot = new ServiceCollection()
                .AddTransient<IMyTransientService, MyTransientService>()
                .AddSingleton<IMySingletonService, MySingletonService>()
                .AddScoped<IMyScopedService, MyScopedOtherService>()
                .AddScoped<IMyScopedService, MyScopedService>()
                .AddSingleton(testSingletonService)
                .BuildServiceProvider();

            while (true)
            {
                Console.WriteLine($"\r\n请输入测试用例编号：{TestFactory.Selections.AsFormatJsonStr()}");
                string num = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(num)) continue;

                ITest test = TestFactory.Create(num);
                test.Run();
            }
        }
    }
}
