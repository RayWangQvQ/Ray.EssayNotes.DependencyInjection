//系统包
using System;
//微软包
using Microsoft.Extensions.DependencyInjection;
//三方包
using Autofac;
using Autofac.Extensions.DependencyInjection;
//本地项目包
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Repository;
using Ray.EssayNotes.AutoFac.Domain.IRepository;

namespace Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc
{
    /// <summary>
    /// Core的AutoFac容器
    /// </summary>
    public static class CoreContainer
    {
        /// <summary>
        /// 容器实例
        /// </summary>
        public static IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IServiceProvider Init(IServiceCollection services, Func<ContainerBuilder, ContainerBuilder> func = null)
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new ContainerBuilder();
            //将Core自带DI容器内的服务迁移到AutoFac容器
            builder.Populate(services);
            //自定义注册组件
            MyBuild(builder);
            func?.Invoke(builder);
            //利用构建器创建容器
            Instance = builder.Build();

            return new AutofacServiceProvider(Instance);
        }

        /// <summary>
        /// 自定义注册
        /// </summary>
        /// <param name="builder"></param>
        public static void MyBuild(this ContainerBuilder builder)
        {
            var assemblies = Helpers.ReflectionHelper.GetAllAssembliesCoreWeb();

            //注册仓储 && Service
            builder.RegisterAssemblyTypes(assemblies)
                .Where(cc => cc.Name.EndsWith("Repository") |//筛选
                             cc.Name.EndsWith("Service"))
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            //注册泛型仓储
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            builder.RegisterType<MyDbContext>()
                .InstancePerLifetimeScope();
                //.InstancePerRequest();

            /*Core不需要注册控制器
            //注册Controller
            Assembly[] controllerAssemblies = assemblies.Where(x => x.FullName.Contains(".CoreApi")).ToArray();
            builder.RegisterAssemblyTypes(controllerAssemblies)
                .Where(cc => cc.Name.EndsWith("Controller"))
                .AsSelf();
                */
        }
    }
}
