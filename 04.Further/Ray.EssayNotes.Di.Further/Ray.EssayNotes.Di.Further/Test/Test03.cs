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
    [Description("根域")]
    public class Test03 : TestBase
    {
        public override void Print()
        {
            //根容器下的引擎
            var engine = Program.ServiceProviderRoot.GetFieldValue("_engine");

            //引擎的Root属性和RootScope属性都指向根域
            var rootScope = engine.GetPropertyValue("RootScope");
            Console.WriteLine($"根引擎域类型：{rootScope.GetType()}({rootScope.GetHashCode()})");

            var fields = rootScope.GetFieldsWithValue()
                .Where(x => !x.Key.Contains("Backing"))
                .ToShowDictionary();
            Console.WriteLine($"根引擎域字段：{fields.AsFormatJsonStr()}");

            var props = rootScope.GetPropertiesWithValue()
                .ToShowDictionary();
            Console.WriteLine($"根引擎域属性：{props.AsFormatJsonStr()}");
        }
    }
}
