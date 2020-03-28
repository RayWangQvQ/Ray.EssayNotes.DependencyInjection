using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister05 : TestRegisterBase
    {
        /// <summary>
        /// 自己指定构造函数
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>()
                .As<IStudentRepository>()
                .AsSelf();

            builder.RegisterType<StudentAppService>()
                .UsingConstructor(typeof(IStudentRepository), typeof(string))
                .As<IStudentAppService>(); ;

            return builder;
        }
    }
}
