using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister04 : TestRegisterBase
    {
        /// <summary>
        /// Register + 委托
        /// 拉姆达表达式注册
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.Register(x => new StudentRepository())
                .As<IStudentRepository>();

            builder.Register(x => new StudentAppService(x.Resolve<IStudentRepository>())//指定参数
            {
                //TeacherRepository = x.Resolve<ITeacherRepository>()//实现【参数注入】（与属性注入不同，这里如果容器找不到解析会报异常，属性注入不会）
            })
                .As<IStudentAppService>();

            return builder;
        }
    }
}
