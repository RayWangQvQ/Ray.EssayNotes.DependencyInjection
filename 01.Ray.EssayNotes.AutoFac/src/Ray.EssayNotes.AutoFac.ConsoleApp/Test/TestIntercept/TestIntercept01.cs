using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.Interceptors;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept
{
    public class TestIntercept01 : TestInterceptBase
    {
        /// <summary>
        /// 注册拦截器类型，在被拦截类上关联拦截器
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.Register(c => new CallLoggerInterceptor(Console.Out));

            builder.RegisterType<TestIntercept01AppService>()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors();

            return builder;
        }

        protected override void PrintResult()
        {
            var service = ConsoleContainer.Instance.Resolve<ITestInterceptAppService>();

            var re = service.DoSomething();
            Console.WriteLine(re);

            service.DoSomethingAnother("123", "456");
        }
    }

    public interface ITestInterceptAppService
    {
        string DoSomething();

        void DoSomethingAnother(string str1, string str2);
    }

    [Intercept(typeof(CallLoggerInterceptor))]
    public class TestIntercept01AppService : ITestInterceptAppService
    {
        public TestIntercept01AppService()
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
