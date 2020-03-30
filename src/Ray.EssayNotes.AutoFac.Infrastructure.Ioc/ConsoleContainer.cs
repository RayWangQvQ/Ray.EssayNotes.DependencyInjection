//系统包
using System;
using System.Reflection;
//三方包
using Autofac;
using Autofac.Core;
//本地项目包
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using Ray.EssayNotes.AutoFac.Infrastructure.Helpers;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc
{
    /// <summary>
    /// 控制台程序容器
    /// </summary>
    public static class ConsoleContainer
    {
        /// <summary>
        /// 容器
        /// </summary>
        public static Autofac.IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="func">委托</param>
        /// <returns></returns>
        public static void Init(Func<Autofac.ContainerBuilder, Autofac.ContainerBuilder> func = null)
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new Autofac.ContainerBuilder();
            //注册组件
            MyBuild(builder);
            func?.Invoke(builder);
            //利用构建器创建容器
            Instance = builder.Build();
        }

        /// <summary>
        /// 自定义注册
        /// </summary>
        /// <param name="builder"></param>
        public static void MyBuild(ContainerBuilder builder)
        {

        }
    }
}
