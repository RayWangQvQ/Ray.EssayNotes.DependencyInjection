using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            LogServiceDescriptors(app.ApplicationServices);

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
            IEnumerable<ServiceDescriptor> descs = serviceProvider.GetServiceDescriptorsFromScope();
            string jsonStr = descs.AsJsonStr(option =>
            {
                option.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };
                option.EnumToString = true;
                option.FilterProps = new Infrastructure.Extensions.Json.FilterPropsOption
                {
                    FilterEnum = Infrastructure.Extensions.Json.FilterEnum.Ignore,
                    Props = new[] { "UsePollingFileWatcher", "Action", "Method", "Assembly" }
                };
            }).AsFormatJsonStr();
            File.WriteAllTextAsync($"./ServiceDescriptorLogs/InitServiceCollections.json", jsonStr);
            return true;
        }
    }
}
