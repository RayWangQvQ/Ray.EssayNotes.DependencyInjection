using System;
using System.Text.Json;
using Ray.EssayNotes.Di.Further.Test;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.Di.Further
{
    class Program
    {
        public static IServiceProvider ServiceProviderRoot { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            while (true)
            {
                Console.WriteLine($"\r\n请输入测试用例编号：{JsonSerializer.Serialize(TestFactory.Selections).AsFormatJsonStr()}");
                string num = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(num)) continue;

                ITest test = TestFactory.Create(num);
                test.Run();
            }
        }
    }
}
