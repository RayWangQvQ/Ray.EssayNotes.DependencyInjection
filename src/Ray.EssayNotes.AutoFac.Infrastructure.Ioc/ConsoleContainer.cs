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

        #region 几种注册特性测试

        /// <summary>
        /// 方法4：指定类型（type）
        /// </summary>
        /// <param name="builder"></param>
        public static void BuildContainerFunc4(ContainerBuilder builder)
        {
            builder.RegisterType<StudentAppService>();
            builder.RegisterType<StudentRepository>();
            //builder.RegisterType(typeof(StudentService));
            //builder.RegisterType(typeof(StudentRepository));
            //还可以自己指定构造函数
            //builder.RegisterType<StudentRepository>()
            //    .UsingConstructor(typeof(StudentRepository));
        }
        /// <summary>
        /// 方法5：属性注入
        /// </summary>
        /// <param name="builder"></param>
        public static void BuildContainerFunc5(ContainerBuilder builder)
        {
            //builder.RegisterType<TeacherRepository>().As<ITeacherRepository>();
            builder.Register(x => new TeacherAppService { TeacherRepository = x.ResolveOptional<ITeacherRepository>() })
                .As<ITeacherService>();

            builder.RegisterType<TeacherAppService>()
                .PropertiesAutowired();
        }
        /// <summary>
        /// 方法6：泛型注入
        /// </summary>
        /// <param name="builder"></param>
        public static void BuildContainerFunc6(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IBaseRepository<>));
            builder.RegisterType<BookAppService>()
                .As<IBookService>();
        }
        /// <summary>
        /// 方法7：已注册内容进行判断
        /// </summary>
        /// <param name="builder"></param>
        public static void BuildContainerFunc7(ContainerBuilder builder)
        {
            //IfNotRegistered
            builder.RegisterType<TeacherRepository>()
                .As<ITeacherRepository>()
                .IfNotRegistered(typeof(ITeacherRepository));

            builder.RegisterType<TeacherRepository>()
                .AsSelf()
                .IfNotRegistered(typeof(ITeacherRepository));

            //OnlyIf
            builder.RegisterType<TeacherAppService>()
                .AsSelf()
                .As<ITeacherService>()
                .OnlyIf(x =>
                    x.IsRegistered(new TypedService(typeof(ITeacherRepository))) ||
                    x.IsRegistered(new TypedService(typeof(TeacherRepository))));
        }
        /// <summary>
        /// 方法8：通过反射程序集
        /// </summary>
        /// <param name="builder"></param>
        public static void BuildContainerFunc8(ContainerBuilder builder)
        {
            Assembly[] assemblies = Helpers.ReflectionHelper.GetAllAssemblies();

            builder.RegisterAssemblyTypes(assemblies)//程序集内所有具象类（concrete classes）
                .Where(cc => cc.Name.EndsWith("Repository") |//筛选
                            cc.Name.EndsWith("Service"))
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                                        //.Except<TeacherRepository>()//排除某类型
                                        //.As(x=>x.GetInterfaces()[0])//反射出其实现的接口，默认以第一个接口类型暴露
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IBaseRepository<>));
        }

        /// <summary>
        /// 暴露类型
        /// </summary>
        /// <param name="builder"></param>
        public static void BuildContainerFunc9(ContainerBuilder builder)
        {
            builder.RegisterType<StudentAppService>();

            builder.RegisterType<StudentAppService>().AsSelf()
                .As<StudentAppService>()
                .As(typeof(StudentAppService))
                .AsImplementedInterfaces();//常用
        }
        #endregion
    }
}
