using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Extensions.MsDi;

namespace Ray.EssayNotes.Di.InitDemo
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

            services.AddSingleton<SingletonService>(sp => new SingletonService(sp));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            LogServiceDescriptors(app.ApplicationServices);
            Console.WriteLine($"app.ApplicationServices:{app.ApplicationServices.GetHashCode()}");

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

        private bool LogServiceDescriptors(IServiceProvider serviceProvider)
        {
            var jsonStr = serviceProvider.SerializeServiceDescriptor(o => { o.IsSerializeImplementationInstance = false; });
            File.WriteAllTextAsync($"./ServiceDescriptorLogs/InitServiceCollections.json", jsonStr);
            return true;
        }
    }
}
