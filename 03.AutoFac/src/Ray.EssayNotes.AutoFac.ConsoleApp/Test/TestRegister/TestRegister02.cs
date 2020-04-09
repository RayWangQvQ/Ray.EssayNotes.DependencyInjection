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
    [Description("注册实例类型（type）和服务类型（interface）")]
    public class TestRegister02 : TestRegisterBase
    {
        /// <summary>
        /// 注册实例类型（type）和服务类型（interface）
        /// 【RegisterType】+【As】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>()//指定实例类型
                .As<IStudentRepository>();//指定服务类型

            builder.RegisterType(typeof(StudentAppService))
                .As(typeof(IStudentAppService))
                .AsSelf()
                .AsImplementedInterfaces();//以其实现的所有接口作为服务类型
            return builder;
        }
    }
}
