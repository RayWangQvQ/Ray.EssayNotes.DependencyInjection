using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.AppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister12 : TestRegisterBase
    {
        protected override void PrintResult()
        {
            var teacherService = ConsoleContainer.Instance.Resolve<TeacherAppService>();
            string s = teacherService.TestStr + teacherService.TestInt;
            Console.WriteLine(s);
        }

        /// <summary>
        /// 属性注入方法3：绑定特定属性值
        /// 【RegisterType】+【WithProperty】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<TeacherRepository>()
                .AsImplementedInterfaces();

            builder.RegisterType<TeacherAppService>()
                .WithProperty("TestStr", "test1")
                .WithProperties(new TypedParameter[]
                {
                    new TypedParameter(typeof(int),9)
                })
                .AsImplementedInterfaces()
                .AsSelf();

            return builder;
        }
    }

}
