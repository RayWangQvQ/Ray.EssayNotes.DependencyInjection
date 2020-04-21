using System;
using System.Text.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.Further.Extensions;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Ray.EssayNotes.Di.Further.Test
{
    [Description("容器中的【服务描述池】")]
    public class Test08 : TestBase
    {
        public override void Print()
        {
            List<ServiceDescriptor> serviceDescriptorList = Program.ServiceProviderRoot
                .GetEngine()
                .GetPropertyValue("CallSiteFactory")
                .GetFieldValue("_descriptors")
                as List<ServiceDescriptor>;

            serviceDescriptorList?.ForEach(x =>
            {
                string s = x.ToString();//ServiceDescriptor重写了ToString()方法

                s = s.Replace("ServiceType: ", "\"ServiceType\":\"");
                s = s.Replace(" Lifetime: ", "\", \"Lifetime\":\"");
                s = s.Replace(" ImplementationType: ", "\", \"ImplementationType\":\"");
                s = $"{{{s}\"}}";

                Console.WriteLine(s.AsFormatJsonStr());
            });



            /**
             * 可以看到，服务描述池在引擎对象中
             * 我们用于注册的ServiceCollection其本质就是一个List<ServiceDescriptor>
             * 当执行Build方法时，这个服务描述集合会封装到CallSiteFactory对象的_descriptors私有字段中，CallSiteFactory作为引擎的一个属性
             * 所以，由于引擎对象的唯一性，服务描述池也是唯一的
             * 这也符合我们的常识，因为对服务的描述是不需要区分范围域，任何一个域需要解析对象，都只需要到同一个服务描述池中获取描述信息就行了
             */

        }

    }
}
