using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using System.Collections.Generic;
using System.ComponentModel;
using Autofac.Core;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    [Description("条件注册（如果没注册过、仅当）")]
    public class TestRegister08 : TestRegisterBase
    {
        protected override void PrintResult()
        {
            string name = "";
            try
            {
                IStudentAppService stuService = MyContainer.Root.Resolve<IStudentAppService>();
                name = stuService.GetStuName(1);
            }
            catch (Exception)
            {
            }
            Console.WriteLine($"输出：{name}");
        }

        /// <summary>
        /// 条件注册（如果没注册过、仅当）
        /// 【IfNotRegistered】、【OnlyIf】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            //IfNotRegistered
            //欲实现：如果容器中没有注册过IStudentRepository，则注册一个，如果已经注册过了，就不注册了
            builder.RegisterType<StudentRepository>()
                .As<IStudentRepository>();
            builder.RegisterType<StudentRepository>()
                .AsSelf()
                .IfNotRegistered(typeof(IStudentRepository));

            //OnlyIf
            //欲实现：先判断容器中是否已经有了IStudentRepository或StudentRepository
            //仅当至少有一个时，才注册Service
            //如果一个仓储都没有，那就不注册这个service了
            builder.RegisterType<StudentAppService>()
                .AsSelf()
                .As<IStudentAppService>()
                .OnlyIf(x =>
                    x.IsRegistered(new TypedService(typeof(IStudentRepository))) ||
                    x.IsRegistered(new TypedService(typeof(StudentRepository))));

            return builder;
        }

        //另外还有一种指定参数的方法，在拉姆达注册里
    }
}
