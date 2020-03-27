using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IService;
using Ray.EssayNotes.AutoFac.Service.Service;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister01 : TestRegisterBase
    {
        /// <summary>
        /// RegisterType + As
        /// 指定实例类型（type）和服务类型（interface）
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>()//指定实例类型
                .As<IStudentRepository>();//指定服务类型

            builder.RegisterType(typeof(StudentService))
                .As(typeof(IStudentService));
            return builder;
        }
    }
}
