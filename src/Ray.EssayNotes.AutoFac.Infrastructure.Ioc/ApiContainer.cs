//系统包
using System;
using System.Linq;
using System.Reflection;
//三方包
using Autofac;
using Autofac.Integration.WebApi;
//本地项目包
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Helpers;
using Ray.EssayNotes.AutoFac.Repository.Repository;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc
{
    /// <summary>
    /// .NET Framework WebApi容器
    /// </summary>
    public static class ApiContainer
    {
        public static Autofac.IContainer Instance;

        /// <summary>
        /// 初始化Api容器
        /// </summary>
        /// <param name="func"></param>
        public static System.Web.Http.Dependencies.IDependencyResolver Init(Func<Autofac.ContainerBuilder, Autofac.ContainerBuilder> func = null)
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new Autofac.ContainerBuilder();
            //注册组件
            MyBuild(builder);
            func?.Invoke(builder);
            //利用构建器创建容器
            Instance = builder.Build();

            //返回针对WebApi的AutoFac解析器
            return new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(Instance);
        }

        /// <summary>
        /// 自定义注册
        /// </summary>
        /// <param name="builder"></param>
        public static void MyBuild(ContainerBuilder builder)
        {
            var assemblies = ReflectionHelper.GetAllAssembliesWebIIS();

            //注册仓储 && Service
            builder.RegisterAssemblyTypes(assemblies)//程序集内所有具象类（concrete classes）
                .Where(cc => cc.Name.EndsWith("Repository") |//筛选
                             cc.Name.EndsWith("Service"))
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            //注册泛型仓储
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            //注册ApiController
            Assembly apiAssembly = assemblies.FirstOrDefault(x => x.FullName.Contains(".NetFrameworkApi"));
            //方法1：自己根据反射注册
            void RegisterControllersOwn(Autofac.ContainerBuilder b)
            {
                b.RegisterAssemblyTypes(apiAssembly)
                    .Where(cc => cc.Name.EndsWith("Controller"))
                    .AsSelf();
            }

            //方法2：用AutoFac提供的专门用于注册ApiController的扩展方法
            void RegisterControllersAutoFac(Autofac.ContainerBuilder b)
            {
                b.RegisterApiControllers(apiAssembly);
            }

            RegisterControllersAutoFac(builder);
        }
    }
}
