using Autofac;
using Autofac.Integration.WebApi;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dependencies;
using Ray.EssayNotes.AutoFac.Repository.IRepository;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc
{
    /// <summary>
    /// .NET Framework WebApi容器
    /// </summary>
    public static class ApiContainer
    {
        public static IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IDependencyResolver Init(Func<ContainerBuilder, ContainerBuilder> func = null)
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new ContainerBuilder();
            //注册组件
            Build(builder);
            func?.Invoke(builder);
            //利用构建器创建容器
            Instance = builder.Build();

            //将AutoFac设置为系统DI解析器，并返回
            IDependencyResolver dependencyResolver = new AutofacWebApiDependencyResolver(Instance);
            return dependencyResolver;
        }

        public static void Build(ContainerBuilder builder)
        {
            var assemblies = Helpers.ReflectionHelper.GetAllAssembliesWeb();

            //注册仓储 && Service
            builder.RegisterAssemblyTypes(assemblies)//程序集内所有具象类（concrete classes）
                .Where(cc => cc.Name.EndsWith("Repository") |//筛选
                             cc.Name.EndsWith("Service"))
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            //注册泛型仓储
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            //注册Controller
            Assembly[] controllerAssemblies = assemblies.Where(x => x.FullName.Contains(".NetFrameworkApi")).ToArray();
            builder.RegisterAssemblyTypes(controllerAssemblies)
                .Where(cc => cc.Name.EndsWith("Controller"))
                .AsSelf();
        }
    }
}
