using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using Newtonsoft.Json;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Model;
using Ray.Infrastructure.Extensions;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions
{
    public static class AutofacExtension
    {
        /// <summary>
        /// 打印组件信息
        /// </summary>
        public static string PrintComponent(this Autofac.IContainer container)
        {
            IComponentRegistry component = container.ComponentRegistry;//注册处
            List<IComponentRegistration> registrations = component.Registrations.ToList();//注册信息表
            registrations.RemoveAt(0);

            return GetComponent(registrations);
        }

        /// <summary>
        /// 打印组件信息
        /// </summary>
        public static string PrintComponent<T>(this Autofac.IContainer container)
        {
            IComponentRegistry component = container.ComponentRegistry;//注册处
            List<IComponentRegistration> registrations = component.RegistrationsFor(new TypedService(typeof(T))).ToList();//注册信息表

            return GetComponent(registrations);
        }

        private static string GetComponent(List<IComponentRegistration> registrations)
        {
            var jSetting = new JsonSerializerSettings
            {
                //NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new LimitPropsContractResolver(new string[] { "Target" }, false)
            };

            //因为属性类型都是接口，不好用AutoMapper，所以用了json
            string jsonStr = JsonConvert.SerializeObject(registrations, jSetting);
            var dtoMyComponentRegistrations = JsonConvert.DeserializeObject<List<ModelComponentRegistration>>(jsonStr);

            for (var i = 0; i < dtoMyComponentRegistrations.Count; i++)
            {
                var item = dtoMyComponentRegistrations[i];
                IComponentRegistration source = registrations[i];
                item.SetSupplyFields(source);
            }

            return JsonConvert.SerializeObject(dtoMyComponentRegistrations, jSetting);
        }
    }
}
