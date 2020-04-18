using System;
using System.ComponentModel;
using System.Linq;
using Ray.EssayNotes.Di.Further.Extensions;

namespace Ray.EssayNotes.Di.Further.Test
{
    [Description("根容器")]
    public class Test01 : TestBase
    {
        public override void Print()
        {
            Console.WriteLine($"根容器类型为：{Program.ServiceProviderRoot.GetType()}（{Program.ServiceProviderRoot.GetHashCode()}）");

            var props = Program.ServiceProviderRoot.GetPropertiesWithValue()
                .ToShowDictionary();
            Console.WriteLine($"属性：{props.AsFormatJsonStr()}");

            var fields = Program.ServiceProviderRoot.GetFieldsWithValue()
                .Where(x => !x.Key.Contains("Backing"))
                .ToShowDictionary();
            Console.WriteLine($"字段：{fields.AsFormatJsonStr()}");
        }
    }
}
