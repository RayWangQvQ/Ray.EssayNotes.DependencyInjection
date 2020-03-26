using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.IService;
using Newtonsoft.Json;
using Ray.EssayNotes.AutoFac.ConsoleApp.Helpers;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public abstract class TestRegisterBase : ITest
    {
        public virtual void Run()
        {
            //初始化容器，将需要用到的组件添加到容器中
            ConsoleContainer.Init(RegisterFunc);

            IStudentService stuService = ConsoleContainer.Instance.Resolve<IStudentService>();
            string name = stuService.GetStuName(1);
            Console.WriteLine(name);

            List<IComponentRegistration> reg = ConsoleContainer.Instance.ComponentRegistry.Registrations.ToList();
            reg.RemoveAt(0);
            var jSetting = new JsonSerializerSettings
            {
                //NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new LimitPropsContractResolver(new string[] { "Target" }, false)
            };
            Console.WriteLine(JsonConvert.SerializeObject(reg, jSetting));
        }

        protected abstract Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder);
    }
}
