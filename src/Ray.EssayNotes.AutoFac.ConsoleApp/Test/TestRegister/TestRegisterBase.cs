using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Newtonsoft.Json;
using Ray.EssayNotes.AutoFac.Infrastructure.Helpers;
using Autofac.Core.Activators.Reflection;

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
            ConsoleContainer.Init(RegisterFunc);

            //打印结果
            PrintResult();

            //打印组件信息
            PrintComponent();
        }

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
            IStudentAppService stuService = ConsoleContainer.Instance.Resolve<IStudentAppService>();
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

            IComponentRegistry component = ConsoleContainer.Instance.ComponentRegistry;//注册处
            List<IComponentRegistration> registrations = component.Registrations.ToList();//注册信息表
            registrations.RemoveAt(0);

            //因为属性类型都是接口，不好用AutoMapper，所以用了json
            string jsonStr = JsonConvert.SerializeObject(registrations, jSetting);
            var dtoMyComponentRegistrations = JsonConvert.DeserializeObject<List<DtoMyComponentRegistration>>(jsonStr);

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
