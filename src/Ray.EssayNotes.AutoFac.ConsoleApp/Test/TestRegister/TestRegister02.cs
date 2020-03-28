using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister02 : TestRegisterBase
    {
        public override void Run()
        {
            //初始化容器，将需要用到的组件添加到容器中
            ConsoleContainer.Init(RegisterFunc);

            IStudentAppService stuService = ConsoleContainer.Instance.Resolve<StudentAppService>();
            string name = stuService.GetStuName(1);

            Console.WriteLine(name);

            PrintComponent();
        }

        /// <summary>
        /// RegisterType
        /// 只指定实例类型（type）
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<StudentAppService>();
            builder.RegisterType<StudentRepository>()
                .AsSelf();

            //除了泛型，还可以传入type
            //builder.RegisterType(typeof(StudentService));
            //builder.RegisterType(typeof(StudentRepository));
            return builder;
        }
    }
}
