using System;
using Ray.EssayNotes.Di.PoolDemo.IServices;

namespace Ray.EssayNotes.Di.PoolDemo.Services
{
    public class MyScopedService : IMyScopedService, IDisposable
    {
        public MyScopedService()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }

        public void Dispose()
        {
            Console.WriteLine($"【Dispose Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }
    }
}
