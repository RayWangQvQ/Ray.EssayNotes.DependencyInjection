//系统包
using System;

namespace Ray.EssayNotes.AutoFac.Repository
{
    public class MyDbContext
    {
        public MyDbContext()
        {
            Console.WriteLine($"【CreateInstance】：{this.GetHashCode()}");
        }
    }
}
