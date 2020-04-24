using System;
using Ray.EssayNotes.Di.PoolDemo.IServices;

namespace Ray.EssayNotes.Di.PoolDemo.Services
{
    public class MySingletonService : IMySingletonService, IDisposable
    {
        public MySingletonService()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }

        public void Dispose()
        {
            Console.WriteLine($"【Dispose Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }
    }
}
