using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister03 : TestRegisterBase
    {
        /// <summary>
        /// RegisterInstance
        /// 注册实例对象
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            var stuRepository = new StudentRepository();
            builder.RegisterInstance(stuRepository).As<IStudentRepository>();

            var stuService = new StudentAppService(stuRepository);
            builder.RegisterInstance(stuService).As<IStudentAppService>();

            return builder;
        }
    }
}
