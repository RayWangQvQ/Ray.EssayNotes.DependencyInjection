using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions;
using Ray.EssayNotes.AutoFac.Service.Dtos;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    public abstract class TestLifetimeScopeBase : ITest
    {
        public virtual void Run()
        {
            //初始化容器，将需要用到的组件添加到容器中
            MyContainer.Init(RegisterFunc);

            //打印注册信息
            PrintComponent();

            //打印结果
            PrintResult();
        }

        public string Title { get; }

        public abstract Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder);

        public virtual void PrintComponent()
        {
            //打印注册信息
            var jsonStr = MyContainer.Root.PrintComponent<DtoToken>();
            Console.WriteLine(jsonStr.AsFormatJsonStr());
        }

        /// <summary>
        /// 打印结果
        /// </summary>
        protected abstract void PrintResult();
    }
}
