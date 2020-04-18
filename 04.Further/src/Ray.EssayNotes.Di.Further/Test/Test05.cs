using System;
using System.Text.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.Further.Extensions;

namespace Ray.EssayNotes.Di.Further.Test
{
    [Description("子域")]
    public class Test05 : TestBase
    {
        public override void Print()
        {
            if (Program.ChildScope1 == null)
            {
                var factory = Program.ServiceProviderRoot.GetRequiredService<IServiceScopeFactory>();
                Program.ChildScope1 = factory.CreateScope();

                /**
                 * 这里是先获取工厂，然后利用工厂生产一个子域
                 * 相当于把Program.ServiceProviderRoot.CreateScope()拆成两步
                 */
            }

            Console.WriteLine($"子域对象：{Program.ChildScope1.GetType()}({Program.ChildScope1.GetHashCode()})");//子域类型是一个ServiceProviderEngineScope对象（引擎域）

            var fields = Program.ChildScope1.GetFieldsWithValue()
                .Where(x => !x.Key.Contains("Backing"))
                .ToShowDictionary();
            Console.WriteLine($"子域（引擎域）字段：{fields.AsFormatJsonStr()}");

            var props = Program.ChildScope1.GetPropertiesWithValue()
                .ToShowDictionary();
            Console.WriteLine($"子域（引擎域）属性：{props.AsFormatJsonStr()}");
        }
    }
}
