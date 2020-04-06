using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Newtonsoft.Json;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Model;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public abstract class TestRegisterBase : ITest
    {
        /// <summary>
        /// 运行测试
        /// </summary>
        public virtual void Run()
        {
            //初始化容器，将需要用到的组件添加到容器中
            MyContainer.Init(RegisterFunc);

            //打印结果
            PrintResult();

            //打印组件信息
            PrintComponent();
        }

        /// <summary>
        /// 测试内容
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected abstract Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder);

        /// <summary>
        /// 打印结果
        /// </summary>
        protected virtual void PrintResult()
        {
            //从容器中解析出对象（可以理解为根生命域(the “root lifetime scope”)
            //这种方式解析出的对象，其生命周期与程序同样长，多了之后可能会造成内存溢出，真实开发场景并不建议使用
            IStudentAppService stuService = MyContainer.Instance.Resolve<IStudentAppService>();
            string name = stuService.GetStuName(1);
            Console.WriteLine($"输出：{name}");
        }

        /// <summary>
        /// 打印组件信息
        /// </summary>
        protected virtual void PrintComponent()
        {
            var jSetting = new JsonSerializerSettings
            {
                //NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new LimitPropsContractResolver(new string[] { "Target" }, false)
            };

            IComponentRegistry component = MyContainer.Instance.ComponentRegistry;//注册处
            List<IComponentRegistration> registrations = component.Registrations.ToList();//注册信息表
            registrations.RemoveAt(0);

            //因为属性类型都是接口，不好用AutoMapper，所以用了json
            string jsonStr = JsonConvert.SerializeObject(registrations, jSetting);
            var dtoMyComponentRegistrations = JsonConvert.DeserializeObject<List<ModelComponentRegistration>>(jsonStr);

            for (var i = 0; i < dtoMyComponentRegistrations.Count; i++)
            {
                var item = dtoMyComponentRegistrations[i];
                IComponentRegistration source = registrations[i];
                item.SetSupplyFields(source);
            }

            Console.WriteLine("注册表信息：");
            Console.WriteLine(JsonConvert.SerializeObject(dtoMyComponentRegistrations, jSetting).AsFormatJsonString());
        }
    }
}
