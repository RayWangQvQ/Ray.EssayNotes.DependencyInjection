using System;
using System.ComponentModel;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    [Description("自己指定构造函数")]
    public class TestRegister04 : TestRegisterBase
    {
        /// <summary>
        /// 自己指定构造函数
        /// 【UsingConstructor】
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
                .UsingConstructor(typeof(StudentRepository), typeof(ITeacherRepository))
                .As<IStudentAppService>();

            return builder;
        }
    }
}
