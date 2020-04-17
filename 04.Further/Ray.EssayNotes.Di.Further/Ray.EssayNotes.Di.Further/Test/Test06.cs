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
    [Description("生成一个子域/子容器，查看其内部引擎")]
    public class Test06 : TestBase
    {
        public override void Print()
        {
            if (Program.ChildScope1 == null)
                Program.ChildScope1 = Program.ServiceProviderRoot.CreateScope();

            //子域下的引擎
            var engine = Program.ChildScope1.GetPropertyValue("Engine");
            Console.WriteLine($"子域下的引擎对象：{engine.GetType()}({engine.GetHashCode()})");

            var engine_fields = engine.GetFieldsWithValue()
                .Where(x => !x.Key.Contains("Backing"))
                .ToShowDictionary();
            Console.WriteLine($"子域下的引擎的字段：{engine_fields.AsFormatJsonStr()}");

            var engine_props = engine.GetPropertiesWithValue()
                .ToShowDictionary();
            Console.WriteLine($"子域下的引擎的属性：{engine_props.AsFormatJsonStr()}");
        }
    }
}
