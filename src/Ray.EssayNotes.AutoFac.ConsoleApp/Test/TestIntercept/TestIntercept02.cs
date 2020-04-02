using System;
using System.Collections.Generic;
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
    public class TestIntercept02 : TestInterceptBase
    {
        /// <summary>
        /// 注册拦截器类型并指定关联标识，在被拦截类上关联标识
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.Register(c => new CallLoggerInterceptor(Console.Out))
                .Named<IInterceptor>("log-calls");

            builder.RegisterType<TestIntercept02AppService>()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors();

            return builder;
        }

        protected override void PrintResult()
        {
            var service = ConsoleContainer.Instance.Resolve<ITestIntercept02AppService>();

            var re = service.DoSomething();
            Console.WriteLine(re);

            service.DoSomethingAnother("123", "456");
        }
    }

    public interface ITestIntercept02AppService
    {
        string DoSomething();

        void DoSomethingAnother(string str1, string str2);
    }

    //[Intercept(typeof(CallLoggerInterceptor))]
    [Intercept("log-calls")]
    public class TestIntercept02AppService : ITestIntercept02AppService
    {
        public TestIntercept02AppService()
        {

        }

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
