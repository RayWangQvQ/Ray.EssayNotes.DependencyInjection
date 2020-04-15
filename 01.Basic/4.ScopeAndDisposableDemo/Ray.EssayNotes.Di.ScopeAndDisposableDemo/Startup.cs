using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ray.EssayNotes.Di.ScopeAndDisposableDemo.IServices;
using Ray.EssayNotes.Di.ScopeAndDisposableDemo.Services;

namespace Ray.EssayNotes.Di.ScopeAndDisposableDemo
{
    public class Startup
    {
        public static IServiceProvider ServiceProviderRoot;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            Test01(services);
            //Test02(services);
            //Test03(services);
            //Test04(services);
            //Test05(services);
        }

        /// <summary>
        /// 测试瞬时实例的释放
        /// </summary>
        /// <param name="services"></param>
        private void Test01(IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
        }

        /// <summary>
        /// 测试域内单例实例释放
        /// </summary>
        /// <param name="services"></param>
        private void Test02(IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }

        /// <summary>
        /// 测试全局单例
        /// </summary>
        /// <param name="services"></param>
        private void Test03(IServiceCollection services)
        {
            //services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IOrderService>(x => new OrderService());
        }

        /// <summary>
        /// 测试全局单例02
        /// </summary>
        /// <param name="services"></param>
        private void Test04(IServiceCollection services)
        {
            Test03(services);
        }

        /// <summary>
        /// 测试全局单例03：自己new出的实例注册，在根容器解析会不会被释放
        /// </summary>
        /// <param name="services"></param>
        private void Test05(IServiceCollection services)
        {
            var instance = new OrderService();
            services.AddSingleton<IOrderService>(instance);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /**系统默认的容器（或者说scpoe作用域）只有两种
             * ApplicationServices：根容器，在系统启动时创建
             * RequestService：请求容器，在程序每次接受到request请求后，由相应负责的中间件创建，请求结束后即释放掉
             */
            ServiceProviderRoot = app.ApplicationServices;//拿到根容器
            //ServiceProviderRoot.GetService<>();//可以从跟容器解析实例

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
