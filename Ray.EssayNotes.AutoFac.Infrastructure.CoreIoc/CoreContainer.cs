using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using Ray.EssayNotes.AutoFac.Repository.IRepository;
using Ray.EssayNotes.AutoFac.Repository.Repository;

namespace Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc
{
    public static class CoreContainer
    {

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
            builder.Populate(services);
            //注册组件
            MyBuild(builder);
            func?.Invoke(builder);
            //利用构建器创建容器
            Instance = builder.Build();
            return new AutofacServiceProvider(Instance);
        }

        public static void MyBuild(this ContainerBuilder builder)
        {
            //builder.RegisterModule(new AutofacModule());

            var assemblies = Helpers.ReflectionHelper.GetAllAssembliesCoreWeb();

            //注册仓储 && Service
            builder.RegisterAssemblyTypes(assemblies)//程序集内所有具象类（concrete classes）
                .Where(cc => cc.Name.EndsWith("Repository") |//筛选
                             cc.Name.EndsWith("Service"))
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            //注册泛型仓储
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            /*
            //注册Controller
            Assembly[] controllerAssemblies = assemblies.Where(x => x.FullName.Contains(".CoreApi")).ToArray();
            builder.RegisterAssemblyTypes(controllerAssemblies)
                .Where(cc => cc.Name.EndsWith("Controller"))
                .AsSelf();
                */
        }
    }
}
