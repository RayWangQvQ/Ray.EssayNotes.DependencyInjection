﻿using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Ray.EssayNotes.AutoFac.Service.Di;

namespace Ray.EssayNotes.AutoFac.NetFrameworkMvc
{
    public class Startup
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureServices(Autofac.ContainerBuilder builder)
        {
            var mvcAssembly = Assembly.GetExecutingAssembly();

            builder.AddRepositories()
                .AddAppServices()
                .RegisterControllers(mvcAssembly);
        }
    }
}
