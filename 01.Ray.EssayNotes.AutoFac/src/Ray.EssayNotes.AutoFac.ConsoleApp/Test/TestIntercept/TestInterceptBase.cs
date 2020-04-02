using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept
{
    public abstract class TestInterceptBase : ITest
    {
        public void Run()
        {
            ConsoleContainer.Init(RegisterFunc);

            PrintComponent();

            PrintResult();
        }

        public string Title { get; }

        public abstract Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder);

        public virtual void PrintComponent()
        {
            string jsonStr = ConsoleContainer.Instance.PrintComponent();
            Console.WriteLine(jsonStr);
        }

        /// <summary>
        /// 打印结果
        /// </summary>
        protected abstract void PrintResult();
    }
}
