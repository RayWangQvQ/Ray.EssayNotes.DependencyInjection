using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using System.Collections.Generic;
using Autofac.Core;
using System.Reflection;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister11 : TestRegisterBase
    {
        public override string Title => "属性注入2";

        protected override void PrintResult()
        {
            var teacherService = MyContainer.Instance.Resolve<ITeacherService>();
            string s = teacherService.GetTeacherName(1);
            Console.WriteLine(s);
        }

        /// <summary>
        /// 属性注入方法2
        /// 【RegisterType】+【PropertiesAutowired】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<TeacherRepository>()
                .AsImplementedInterfaces();

            builder.RegisterType<TeacherAppService>()
                .PropertiesAutowired()
                .AsImplementedInterfaces();

            return builder;
        }
    }
}
