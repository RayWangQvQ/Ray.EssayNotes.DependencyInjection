//微软包
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//三方包
using Autofac;
//本地项目包
using Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc;
using Microsoft.Extensions.Hosting;

namespace Ray.EssayNotes.AutoFac.CoreApi
{
    public class StartupWithAutoFac
    {
        public IConfiguration Configuration { get; }

        public StartupWithAutoFac(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        /// <summary>
        /// 利用该方法可以使用AutoFac辅助注册，该方法在ConfigureServices()之后执行，所以当发生覆盖注册时，以后者为准。
        /// 不要再利用构建起去创建AutoFac容器了，系统已经接管了。
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.MyBuild();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
