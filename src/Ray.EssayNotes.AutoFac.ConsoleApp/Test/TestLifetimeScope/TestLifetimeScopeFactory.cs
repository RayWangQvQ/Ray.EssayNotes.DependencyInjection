using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Infrastructure.Extensions;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    public class TestLifetimeScopeFactory : ITestFactory
    {
        public ITest Create(string num)
        {
            string classFullName = $"Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope.TestLifetimeScope{num}";
            Type type = Type.GetType(classFullName);
            dynamic obj = type?.Assembly.CreateInstance(classFullName);
            return obj;
        }

        public string TestType => "生命周期作用域";

        public string GetSelectionRange => "01-07";
    }
}
