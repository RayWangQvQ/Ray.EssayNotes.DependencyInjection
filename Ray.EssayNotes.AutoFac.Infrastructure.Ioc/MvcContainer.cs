using Autofac;
using Autofac.Integration.Mvc;
using Ray.EssayNotes.AutoFac.Repository.IRepository;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc
{
    /// <summary>
    /// .net framework MVC程序容器
    /// </summary>
    public static class MvcContainer
    {
        public static IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static void Init(Func<ContainerBuilder, ContainerBuilder> func = null)
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new ContainerBuilder();
            //注册组件
            Build(builder);
            func?.Invoke(builder);
            //利用构建器创建容器
            Instance = builder.Build();

            //将AutoFac设置为系统DI解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Instance));
        }

        public static void Build(ContainerBuilder builder)
        {
            Assembly[] assemblies = Helpers.ReflectionHelper.GetAllAssembliesWeb();

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
            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(cc => cc.Name.EndsWith("Controller"))
            //    .AsSelf();
            //或者可以用AutoFac封装好的一个专门用来注册Controller的方法
            Assembly mvcAssembly = assemblies.FirstOrDefault(x => x.FullName.Contains(".NetFrameworkMvc"));
            builder.RegisterControllers(mvcAssembly);
        }
    }
}
