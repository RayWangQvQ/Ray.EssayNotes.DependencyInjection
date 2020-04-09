using System;
using System.ComponentModel;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.Interceptors;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept
{
    [Description("在注册时关联拦截器")]
    public class TestIntercept04 : TestInterceptBase
    {
        /// <summary>
        /// 在注册被拦截组件时直接关联具体的拦截器，被拦截类和接口都不需要再关联
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.Register(c => new CallLoggerInterceptor(Console.Out));

            builder.RegisterType<TestIntercept04AppService>()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CallLoggerInterceptor));

            return builder;
        }

        protected override void PrintResult()
        {
            var service = MyContainer.Root.Resolve<ITestIntercept04AppService>();

            var re = service.DoSomething();
            Console.WriteLine(re);

            service.DoSomethingAnother("123", "456");
        }
    }

    public interface ITestIntercept04AppService
    {
        string DoSomething();

        void DoSomethingAnother(string str1, string str2);
    }

    public class TestIntercept04AppService : ITestIntercept04AppService
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
