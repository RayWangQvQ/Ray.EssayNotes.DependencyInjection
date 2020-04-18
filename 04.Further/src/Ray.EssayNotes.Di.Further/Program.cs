using System;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.Further.Test;

namespace Ray.EssayNotes.Di.Further
{
    class Program
    {
        public static IServiceProvider ServiceProviderRoot { get; set; }

        public static IServiceScope ChildScope1 { get; set; }

        public static IServiceScope ChildScope2 { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            while (true)
            {
                Console.WriteLine($"\r\n请输入测试用例编号：{TestFactory.Selections.AsFormatJsonStr()}");
                string num = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(num)) continue;

                ITest test = TestFactory.Create(num);
                test.Run();
            }
        }
    }
}
