using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using System.Collections.Generic;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister05 : TestRegisterBase
    {
        /// <summary>
        /// 注册时指定参数（注册类型 + 参数）
        /// 【WithParameter】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>()
                .As<IStudentRepository>()
                .AsSelf();

            builder.RegisterType<TeacherRepository>()
                .As<ITeacherRepository>();

            builder.RegisterType<StudentAppService>()
                .UsingConstructor(typeof(StudentRepository), typeof(ITeacherRepository), typeof(string))
                //.WithParameter("test", "123")//方法1.直接指定参数名称和参数值
                .WithParameter(new TypedParameter(typeof(string), "456"))//方法2.传入Parameter对象
                //.WithParameter((pi, cc) => pi.ParameterType == typeof(string) && pi.Name == "test", (pi, cc) => "789")//方法3.传入两个委托分别指定筛选参数的方法和参数值
                //.WithParameters(new List<TypedParameter> { new TypedParameter(typeof(string), "110") })//方法4.传入Parameter对象集合（参考方法2）
                .As<IStudentAppService>(); ;

            return builder;
        }

        //另外还有一种指定参数的方法，在拉姆达注册里
    }
}
