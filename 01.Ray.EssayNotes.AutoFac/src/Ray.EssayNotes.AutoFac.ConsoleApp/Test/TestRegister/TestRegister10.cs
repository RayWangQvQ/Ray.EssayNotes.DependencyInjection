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
    public class TestRegister10 : TestRegisterBase
    {
        public override string Title => "属性注入1";

        protected override void PrintResult()
        {
            var teacherService = MyContainer.Instance.Resolve<ITeacherService>();
            string s = teacherService.GetTeacherName(1);
            Console.WriteLine(s);
        }

        /// <summary>
        /// 属性注入方法1
        /// 拉姆达
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.Register(x => new TeacherRepository
            {
                TestStr = "test"
            })
                .AsImplementedInterfaces();

            builder.Register(x => new TeacherAppService())
                .OnActivated(e => e.Instance.TeacherRepository = e.Context.Resolve<ITeacherRepository>())
                .AsImplementedInterfaces();

            return builder;

            //拉姆达式属性注入（与属性注入不同，如果容器找不到，Resolve解析会报异常，属性注入不会）
        }
    }
}
