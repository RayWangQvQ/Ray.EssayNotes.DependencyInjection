using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extensions.DependencyInjection;

namespace Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc.Extensions
{
    public static class AutoFacExtension
    {
        public static IWebHostBuilder HookAutoFacIntoPipeline(this IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => services.AddAutofac());
            return builder;
        }
    }
}
