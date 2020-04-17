using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ray.EssayNotes.Di.Further.Test
{
    public static class TestFactory
    {
        public static ITest Create(string num)
        {
            string fullName = $"Ray.EssayNotes.Di.Further.Test.Test{num}";

            Assembly assembly = Assembly.GetExecutingAssembly();

            return (ITest)assembly.CreateInstance(fullName);
        }

        public static Dictionary<string, string> Selections => Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsSubclassOf(typeof(TestBase)))
            .ToDictionary(x => x.Name.Substring(x.Name.Length - 2),
                x => x.GetCustomAttribute<DescriptionAttribute>().Description);
    }
}
