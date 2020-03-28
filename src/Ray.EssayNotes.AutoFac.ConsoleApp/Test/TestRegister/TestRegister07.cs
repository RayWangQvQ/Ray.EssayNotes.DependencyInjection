using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using System.Collections.Generic;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister07 : TestRegisterBase
    {
        protected override void PrintResult()
        {
            IBookService bookService = ConsoleContainer.Instance.Resolve<IBookService>();
            string tite = bookService.GetTitle(1);
            Console.WriteLine($"输出：{tite}");
        }

        /// <summary>
        /// 注册泛型
        /// 【RegisterGeneric】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IBaseRepository<>));
            builder.RegisterType<BookAppService>()
                .As<IBookService>();

            return builder;
        }

        //另外还有一种指定参数的方法，在拉姆达注册里
    }
}
