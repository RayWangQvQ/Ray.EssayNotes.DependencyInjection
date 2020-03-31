using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    public class TestLifetimeScopeFactory
    {
        public static TestLifetimeScopeBase Create(string num)
        {
            string classFullName = $"Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope.TestLifetimeScope{num}";
            Type type = Type.GetType(classFullName);
            dynamic obj = type?.Assembly.CreateInstance(classFullName);
            return obj;
        }
    }
}
