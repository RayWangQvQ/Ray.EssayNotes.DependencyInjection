using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    [Description("注册泛型")]
    public class TestRegister07 : TestRegisterBase
    {
        protected override void PrintResult()
        {
            IBookService bookService = RayContainer.AutofacRootScope.Resolve<IBookService>();
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
    }
}
