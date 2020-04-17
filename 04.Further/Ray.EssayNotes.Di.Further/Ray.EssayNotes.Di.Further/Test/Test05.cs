using System;
using System.Text.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Ray.Infrastructure.Extensions;
using Ray.EssayNotes.Di.Further.Extensions;

namespace Ray.EssayNotes.Di.Further.Test
{
    [Description("生成一个子域/子容器，查看其内部引擎")]
    public class Test05 : TestBase
    {
        public override void Print()
        {
            using (IServiceScope sub = Program.ServiceProviderRoot.CreateScope())
            {
                Console.WriteLine($"子域对象：{sub.GetType()}({sub.GetHashCode()})");//子域类型是一个ServiceProviderEngineScope对象（引擎域）

                var fields = sub.GetFieldsWithValue()
                    .Where(x => !x.Key.Contains("Backing"))
                    .ToShowDictionary();
                Console.WriteLine($"子域（引擎域）字段：{fields.AsFormatJsonStr()}");

                var props = sub.GetPropertiesWithValue()
                    .ToShowDictionary(); ;
                Console.WriteLine($"子域（引擎域）属性：{props.AsFormatJsonStr()}");

                //子域下的引擎
                var engine = sub.GetPropertyValue("Engine");
                Console.WriteLine($"引擎对象：{engine.GetType()}({engine.GetHashCode()})");

                var engine_fields = engine.GetFieldsWithValue()
                    .Where(x => !x.Key.Contains("Backing"))
                    .ToShowDictionary();
                Console.WriteLine($"引擎字段：{engine_fields.AsFormatJsonStr()}");

                var engine_props = engine.GetPropertiesWithValue()
                    .ToShowDictionary();
                Console.WriteLine($"引擎属性：{engine_props.AsFormatJsonStr()}");
            }
        }
    }
}
