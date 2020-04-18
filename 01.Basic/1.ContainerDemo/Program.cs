using System;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.ContainerDemo.IServices;
using Ray.EssayNotes.Di.ContainerDemo.Services;

namespace Ray.EssayNotes.Di.ContainerDemo
{
    class Program
    {
        /// <summary>
        /// 容器
        /// </summary>
        public static IServiceProvider ServiceProvider;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //1.初始化容器
            InitContainer();

            void GetAndPrintTwice<T>(IServiceProvider serviceProvider)
            {
                for (int i = 1; i < 3; i++)
                {
                    T instance = serviceProvider.GetService<T>();
                    Console.WriteLine($"【第{i}次】获取到{instance.GetType().Name}：{instance.GetHashCode()}");
                }
                Console.WriteLine();
            }

            //2.在根容器中，获取实例（每个获取两次）
            Console.WriteLine("瞬时实例:");
            GetAndPrintTwice<IMyTransientService>(ServiceProvider);
            Console.WriteLine("域内单例:");
            GetAndPrintTwice<IMyScopedService>(ServiceProvider);
            Console.WriteLine("全局单例:");
            GetAndPrintTwice<IMySingletonService>(ServiceProvider);

            //3.在子容器中，获取实例（每个获取两次）
            using (var sub = ServiceProvider.CreateScope())
            {
                Console.WriteLine("瞬时实例:");
                GetAndPrintTwice<IMyTransientService>(sub.ServiceProvider);
                Console.WriteLine("域内单例:");
                GetAndPrintTwice<IMyScopedService>(sub.ServiceProvider);
                Console.WriteLine("全局单例:");
                GetAndPrintTwice<IMySingletonService>(sub.ServiceProvider);
            }
        }

        /// <summary>
        /// 初始化容器（注册）
        /// </summary>
        private static void InitContainer()
        {
            //1.实例化注册ServiceCollection，用于注册，相当于容器的builder
            var services = new ServiceCollection();

            //2.注册
            services.AddTransient<IMyTransientService, MyTransientService>();//瞬时实例
            services.AddScoped<IMyScopedService, MyScopedService>();//域内单例
            services.AddSingleton<IMySingletonService, MySingletonService>();//全局单例

            //3.构建容器
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
