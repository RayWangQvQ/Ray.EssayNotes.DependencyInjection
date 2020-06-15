using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using System.ComponentModel;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    [Description("注册实例类型（type）")]
    public class TestRegister01 : TestRegisterBase
    {
        public override void Run()
        {
            //初始化容器，将需要用到的组件添加到容器中
            RayContainer.Init(RegisterFunc);

            IStudentAppService stuService = RayContainer.AutofacRootScope.Resolve<StudentAppService>();
            string name = stuService.GetStuName(1);

            Console.WriteLine(name);

            PrintComponent();
        }

        /// <summary>
        /// 注册实例类型（type）
        /// 【RegisterType】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>();
            builder.RegisterType<StudentAppService>();

            //除了泛型，还可以传入type
            //builder.RegisterType(typeof(StudentService));
            //builder.RegisterType(typeof(StudentRepository));
            return builder;
        }
    }
}
