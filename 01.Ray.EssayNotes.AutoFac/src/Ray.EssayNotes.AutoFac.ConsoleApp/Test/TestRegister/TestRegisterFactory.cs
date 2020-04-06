using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegisterFactory : ITestFactory
    {
        public ITest Create(string num)
        {
            string classFullName = $"Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister.TestRegister{num}";

            Type type = Type.GetType(classFullName);
            ITest obj = (ITest)type?.Assembly.CreateInstance(classFullName);

            return obj;
        }

        public string TestType => "注册";

        public string GetSelectionRange => "01 - 12";
    }
}
