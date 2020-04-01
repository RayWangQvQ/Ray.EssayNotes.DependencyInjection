//系统包
using System;
//三方包
using Ray.EssayNotes.AutoFac.ConsoleApp.Test;
using Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope;
using Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister;

namespace Ray.EssayNotes.AutoFac.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestRegister();

            TestLifetimeScope();
        }

        private static void TestRegister()
        {
            while (true)
            {
                Console.WriteLine("\r\n请输入【注册】测试编号(01-12)：");

                string testNum = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(testNum)) continue;
                ITest test = TestRegisterFactory.Create(testNum);
                test.Run();
            }
        }

        private static void TestLifetimeScope()
        {
            while (true)
            {
                Console.WriteLine("\r\n请输入【生命周期】测试编号(01-06)：");

                string testNum = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(testNum)) continue;
                ITest test = TestLifetimeScopeFactory.Create(testNum);
                test.Run();
            }
        }
    }
}

