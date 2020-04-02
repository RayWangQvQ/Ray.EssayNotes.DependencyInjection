//系统包
using System;
using System.Collections.Generic;
//三方包
using Ray.EssayNotes.AutoFac.ConsoleApp.Test;
using Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestIntercept;
using Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope;
using Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister;

namespace Ray.EssayNotes.AutoFac.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new Dictionary<string, ITestFactory>
            {
                {"1", new TestRegisterFactory()},
                {"2", new TestLifetimeScopeFactory()},
                {"3", new TestInterceptFactory() },
            };

            while (true)
            {
                Console.WriteLine("\r\n请输入要测试的类型：\r\n1.测试注册\r\n2.测试生命周期\r\n3.测试拦截器");
                string key = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(key)) continue;
                bool isSuccess = dic.TryGetValue(key, out ITestFactory factory);
                if (!isSuccess) continue;

                Console.WriteLine($"\r\n请输入【{factory.TestType}】测试编号：({factory.SelectionRange})：");
                string testNum = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(testNum)) continue;
                ITest test = factory.Create(testNum);
                test.Run();
            }
        }
    }
}

