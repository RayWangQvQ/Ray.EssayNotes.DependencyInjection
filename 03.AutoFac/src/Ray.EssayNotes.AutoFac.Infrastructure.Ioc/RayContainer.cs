using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc
{
    public static class RayContainer
    {
        private static ILifetimeScope _autofacRootScope;

        /// <summary>
        /// MsDi根容器(引擎域)
        /// </summary>
        public static IServiceProvider ServiceProviderRoot { get; set; }

        /// <summary>
        /// Autofac根域
        /// </summary>
        public static ILifetimeScope AutofacRootScope => _autofacRootScope ?? ServiceProviderRoot.GetAutofacRoot();

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="func">委托</param>
        /// <returns></returns>
        public static void Init(Func<ContainerBuilder, ContainerBuilder> func = null)
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new ContainerBuilder();

            //注册组件
            func?.Invoke(builder);

            //利用构建器创建容器
            _autofacRootScope = builder.Build();
        }
    }
}
