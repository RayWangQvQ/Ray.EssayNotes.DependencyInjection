using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test
{
    /// <summary>
    /// 抽象工厂
    /// </summary>
    public abstract class TestFactory
    {
        public abstract Type TestBaseType { get; }

        public virtual string SelectionRange
        {
            get
            {
                var dic = new Dictionary<string, string>();
                Assembly assembly = Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes()
                    .Where(x => x.BaseType == this.TestBaseType
                                && x.IsClass);
                foreach (var type in types)
                {
                    string desc = type.GetCustomAttribute<DescriptionAttribute>()?.Description
                                  ?? "";
                    dic.Add(type.Name.Substring(type.Name.Length - 2), desc);
                }

                dic = dic.OrderBy(x => x.Key)
                    .ToDictionary(p => p.Key, p => p.Value);

                return JsonConvert.SerializeObject(dic)
                    .AsFormatJsonStr()
                    .Trim('{', '}');
            }
        }

        public virtual ITest Create(string num)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes()
                .FirstOrDefault(x => x.BaseType == this.TestBaseType
                                     && x.IsClass
                                     && x.Name.EndsWith(num));
            dynamic obj = type?.Assembly.CreateInstance(type.FullName);
            return obj;
        }
    }
}
