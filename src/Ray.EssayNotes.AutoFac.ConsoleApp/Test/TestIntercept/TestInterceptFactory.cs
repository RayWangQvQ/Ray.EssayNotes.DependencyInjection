using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Infrastructure.Extensions;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept
{
    public class TestInterceptFactory : ITestFactory
    {
        private static Dictionary<string, string> _remarkDic = new Dictionary<string, string>();

        public ITest Create(string num)
        {
            string classFullName = $"Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept.TestIntercept{num}";
            Type type = Type.GetType(classFullName);
            dynamic obj = type?.Assembly.CreateInstance(classFullName);
            return obj;
        }

        public string TestType => "拦截器";

        public string SelectionRange => _remarkDic.GetSelectionToStr();
    }
}
