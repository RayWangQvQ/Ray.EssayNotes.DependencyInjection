using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IService;
using Ray.EssayNotes.AutoFac.Service.Service;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister04 : TestRegisterBase
    {
        /// <summary>
        /// 拉姆达表达式注册
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.Register(x => new StudentRepository())
                .As<IStudentRepository>();

            builder.Register(x => new StudentService(x.Resolve<IStudentRepository>()))
                .As<IStudentService>();

            return builder;
        }
    }
}
