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
    [Description("子域")]
    public class Test04 : TestBase
    {
        public override void Print()
        {
            using (IServiceScope sub = Program.ServiceProviderRoot.CreateScope())
            {
                Console.WriteLine($"子域类型：{sub.GetType()}({sub.GetHashCode()})");//子域类型是一个ServiceProviderEngineScope对象（引擎域）

                var fields = sub.GetFieldsWithValue()
                    .Where(x => !x.Key.Contains("Backing"))
                    .ToShowDictionary();
                Console.WriteLine($"子域（引擎域）字段：{fields.AsFormatJsonStr()}");

                var props = sub.GetPropertiesWithValue()
                    .ToShowDictionary(); ;
                Console.WriteLine($"子域（引擎域）属性：{props.AsFormatJsonStr()}");
            }
        }
    }
}
