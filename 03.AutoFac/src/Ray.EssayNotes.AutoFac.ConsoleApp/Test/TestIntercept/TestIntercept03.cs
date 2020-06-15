using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.AppService;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.Interceptors;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept
{
    [Description("在interface接口上关联拦截器")]
    public class TestIntercept03 : TestInterceptBase
    {
        /// <summary>
        /// 注册拦截器，在被拦截类的实现接口上关联拦截器
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.Register(c => new CallLoggerInterceptor(Console.Out));

            builder.RegisterType<TestIntercept03AppService>()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors();

            return builder;
        }

        protected override void PrintResult()
        {
            var service = RayContainer.AutofacRootScope.Resolve<ITestIntercept03AppService>();

            var re = service.DoSomething();
            Console.WriteLine(re);

            service.DoSomethingAnother("123", "456");
        }
    }

    [Intercept(typeof(CallLoggerInterceptor))]
    public interface ITestIntercept03AppService
    {
        string DoSomething();

        void DoSomethingAnother(string str1, string str2);
    }

    public class TestIntercept03AppService : ITestIntercept03AppService
    {
        public string DoSomething()
        {
            return "Do Something successfully!";
        }

        public void DoSomethingAnother(string str1, string str2)
        {
            return;
        }
    }
}
