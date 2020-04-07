using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister06 : TestRegisterBase
    {
        public override string Title => "拉姆达表达式注册";

        /// <summary>
        /// 拉姆达表达式注册
        /// 【Register】+ 委托
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.Register(x => new StudentRepository())
                .As<IStudentRepository>();

            builder.Register(x => new StudentAppService(x.Resolve<IStudentRepository>())//指定参数
            {
                //TeacherRepository = x.Resolve<ITeacherRepository>()//拉姆达式属性注入（与属性注入不同，这里如果容器找不到解析会报异常，属性注入不会）
            })
                .As<IStudentAppService>();

            return builder;
        }
    }
}
