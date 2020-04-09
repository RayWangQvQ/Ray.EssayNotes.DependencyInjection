using System;
using System.ComponentModel;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.Interceptors;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept
{
    [Description("类拦截器（EnableClassInterceptors）")]
    public class TestIntercept07 : TestInterceptBase
    {
        /// <summary>
        /// 类拦截器
        /// （如果注册的服务没有实现接口，则需要使用类拦截器）
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.Register(c => new CallLoggerMethodInterceptor(Console.Out));

            builder.RegisterType<TestIntercept07AppService>()
                .AsSelf()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(CallLoggerMethodInterceptor));

            return builder;
        }

        protected override void PrintResult()
        {
            var service = MyContainer.Root.Resolve<TestIntercept07AppService>();

            var re = service.DoSomething();
            Console.WriteLine(re);

            service.DoSomethingAnother("123", "456");
        }
    }

    public class TestIntercept07AppService
    {
        [CallLogger]
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
