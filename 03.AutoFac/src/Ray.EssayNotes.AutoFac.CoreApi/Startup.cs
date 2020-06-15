using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository;
using Ray.EssayNotes.AutoFac.Service.Di;

namespace Ray.EssayNotes.AutoFac.CoreApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //.AddControllersAsServices();
        }

        /// <summary>
        /// Autofac注册
        /// 不需要执行build构建容器，构建的工作由Core框架完成
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddRepositories()
                .AddAppServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //存储根容器的引用，方便以后调用
            RayContainer.ServiceProviderRoot = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Test01();
            Test02();
        }

        private void Test01()
        {
            Console.WriteLine($"{RayContainer.ServiceProviderRoot.GetType()}:{RayContainer.ServiceProviderRoot.GetHashCode()}");
            /*
             * 拿到的是Autofac.Extensions.DependencyInjection.AutofacServiceProvider对象
             */

            Console.WriteLine($"{RayContainer.AutofacRootScope.GetType()}:{RayContainer.AutofacRootScope.GetHashCode()}");
            /*
             * 拿到的是Autofac.Core.Lifetime.LifetimeScope
             */
        }

        private void Test02()
        {
            MyDbContext dbContext1 = RayContainer.ServiceProviderRoot.GetService<MyDbContext>();
            MyDbContext dbContext2 = RayContainer.AutofacRootScope.Resolve<MyDbContext>();

            Console.WriteLine($"{dbContext1.GetHashCode()}");
            Console.WriteLine($"{dbContext2.GetHashCode()}");
        }
    }
}
