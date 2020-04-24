using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Ray.EssayNotes.Di.PoolDemo.Test
{
    [Description("容器中的【服务描述池】")]
    public class Test01 : TestBase
    {
        public override void Run()
        {
            List<ServiceDescriptor> serviceDescriptorList = Program.ServiceProviderRoot
                .GetEngine()//拿到引擎对象
                .GetPropertyValue("CallSiteFactory")//拿到引擎的CallSiteFactory属性（CallSiteFactory对象）
                .GetFieldValue("_descriptors")//CallSiteFactory对象内的_descriptors字段即是服务描述集合
                as List<ServiceDescriptor>;

            serviceDescriptorList?.ForEach(x =>
            {
                Console.WriteLine(x.ToString());//ServiceDescriptor重写了ToString()方法
            });


            /**
             * 可以看到，服务描述池在引擎对象中
             * 我们用于注册的ServiceCollection其本质就是一个List<ServiceDescriptor>
             * 当执行Build方法时，这个服务描述集合会封装到CallSiteFactory对象的_descriptors私有字段中，CallSiteFactory作为引擎的一个属性
             * 所以，由于引擎对象的唯一性，服务描述池也是唯一的
             * 这也符合我们的常识，因为对服务的描述是不需要区分范围域，当任何一个域需要解析对象，都只需要到这个服务描述池中获取描述信息就行了
             */

        }

    }
}
