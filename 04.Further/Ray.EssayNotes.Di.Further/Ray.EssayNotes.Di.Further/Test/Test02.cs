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
    [Description("根容器下的引擎")]
    public class Test02 : TestBase
    {
        public override void Print()
        {
            var engine = Program.ServiceProviderRoot.GetFieldValue("_engine");
            Console.WriteLine($"根容器下的引擎对象：{engine.GetType().FullName}({engine.GetHashCode()})");

            var fields = engine.GetFieldsWithValue()
                .Where(x => !x.Key.Contains("Backing"))
                .ToShowDictionary();
            Console.WriteLine($"引擎字段：{fields.AsFormatJsonStr()}");

            var props = engine.GetPropertiesWithValue()
                .ToShowDictionary(); ;
            Console.WriteLine($"引擎属性：{props.AsFormatJsonStr()}");
        }
    }
}
