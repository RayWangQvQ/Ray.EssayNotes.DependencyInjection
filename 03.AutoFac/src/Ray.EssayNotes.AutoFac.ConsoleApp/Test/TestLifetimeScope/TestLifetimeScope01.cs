using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.AppService;
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    public class TestLifetimeScope01 : TestLifetimeScopeBase
    {
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>().AsImplementedInterfaces();
            builder.RegisterType<StudentAppService>().AsImplementedInterfaces();

            return builder;
        }

        /// <summary>
        /// 从根容器创建一个子域
        /// </summary>
        protected override void PrintResult()
        {
            // 创建一个子生命域, 在其内部解析对象，使用完后, 所有解析资源会随子域一起释放掉
            using (var scope = MyContainer.Instance.BeginLifetimeScope())
            {
                var stuService = scope.Resolve<IStudentAppService>();
                string s = stuService.GetStuName(1);
                Console.WriteLine(s);
            }
        }
    }
}
