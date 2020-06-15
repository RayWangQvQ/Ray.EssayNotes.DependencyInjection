using System;
using System.ComponentModel;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.Interceptors;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept
{
    [Description("针对类内具体方法的拦截-（标记interface）")]
    public class TestIntercept06 : TestInterceptBase
    {
        /// <summary>
        /// 实现针对类内具体方法的拦截（标记interface）
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.Register(c => new CallLoggerMethodInterceptor(Console.Out));

            builder.RegisterType<TestIntercept06AppService>()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CallLoggerMethodInterceptor));

            return builder;
        }

        protected override void PrintResult()
        {
            var service = RayContainer.AutofacRootScope.Resolve<ITestIntercept06AppService>();

            var re = service.DoSomething();
            Console.WriteLine(re);

            service.DoSomethingAnother("123", "456");
        }
    }

    public interface ITestIntercept06AppService
    {
        [CallLogger]
        string DoSomething();

        void DoSomethingAnother(string str1, string str2);
    }

    public class TestIntercept06AppService : ITestIntercept06AppService
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
