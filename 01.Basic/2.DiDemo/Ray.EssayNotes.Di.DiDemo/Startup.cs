using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Ray.EssayNotes.Di.DiDemo.Dtos;
using Ray.EssayNotes.Di.DiDemo.IServices;
using Ray.EssayNotes.Di.DiDemo.Services;

namespace Ray.EssayNotes.Di.DiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Test01(services);
            //Test02(services);
            //Test03(services);
            //Test04(services);
            //Test05(services);
            //Test06(services);
            //Test07(services);
            //Test08(services);
            Test09(services);
        }

        /// <summary>
        /// 测试构造注入
        /// </summary>
        private void Test01(IServiceCollection services)
        {
            services.AddTransient<IMyTransientService, MyTransientService>();
            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddSingleton<IMySingletonService, MySingletonService>();
        }

        /// <summary>
        /// 测试参数注入
        /// </summary>
        private void Test02(IServiceCollection services)
        {
            Test01(services);
        }

        /// <summary>
        /// 测试new实例注册
        /// </summary>
        private void Test03(IServiceCollection services)
        {
            var dto = new MyDto();

            //只有注册单例生命周期才能直接使用已实例化的对象，这是会将实例对象直接持久化到容器
            //以下两种写法相同效果
            services.AddSingleton(dto);
            services.AddSingleton<MyDto>(dto);

            //以下写法不存在，生命周期为瞬时和域内单例的，注册时只能提供构造方法或委托，不能直接注册实例
            //services.AddTransient(dto);
            //services.AddScoped(dto);
        }

        /// <summary>
        /// 委托注册
        /// </summary>
        private void Test04(IServiceCollection services)
        {
            services.AddScoped(serviceProvider => new MyDto());
        }

        /// <summary>
        /// 利用注册委托实现属性注入
        /// </summary>
        private void Test05(IServiceCollection services)
        {
            //services.AddScoped(serviceProvider => new MyDto());

            services.AddScoped(serviceProvider => new OtherDto
            {
                MyDto = serviceProvider.GetService<MyDto>()
            });
        }

        /// <summary>
        /// 尝试注册
        /// </summary>
        private void Test06(IServiceCollection services)
        {
            Test01(services);
            services.TryAddScoped<IMyScopedService, OtherScopedService>();//容器中如果已存在IMyScopedService，就不注册，如果不存在，就正常注册

            //以下两种写法和上面效果相同
            //services.TryAdd(new ServiceDescriptor(typeof(IMyScopedService), x => new OtherScopedService(), ServiceLifetime.Scoped));
            //services.TryAdd(ServiceDescriptor.Scoped<IMyScopedService, OtherScopedService>());
        }

        /// <summary>
        /// 尝试注册多实现服务
        /// </summary>
        private void Test07(IServiceCollection services)
        {
            Test01(services);

            services.TryAddEnumerable(ServiceDescriptor.Scoped<IMyScopedService, OtherScopedService>());//如果服务和实例类型都相同，就不会再次注册；如果不同，就会注册
        }

        /// <summary>
        /// 移除/替换
        /// </summary>
        private void Test08(IServiceCollection services)
        {
            Test01(services);

            services.Replace(ServiceDescriptor.Scoped<IMyScopedService, OtherScopedService>());

            services.RemoveAll<IMyScopedService>();
        }

        /// <summary>
        /// 注册泛型
        /// </summary>
        private void Test09(IServiceCollection services)
        {
            Test01(services);

            services.AddScoped(typeof(GenericDto<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
