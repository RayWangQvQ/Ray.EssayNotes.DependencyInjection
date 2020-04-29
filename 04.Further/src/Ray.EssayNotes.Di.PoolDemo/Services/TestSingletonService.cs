using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.EssayNotes.Di.PoolDemo.Services
{
    public class TestSingletonService : IDisposable
    {
        public TestSingletonService()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }

        public void Dispose()
        {
            Console.WriteLine($"【Dispose Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }
    }
}
