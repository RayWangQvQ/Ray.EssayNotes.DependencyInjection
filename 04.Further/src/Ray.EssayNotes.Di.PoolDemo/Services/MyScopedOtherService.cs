using System;
using Ray.EssayNotes.Di.PoolDemo.IServices;

namespace Ray.EssayNotes.Di.PoolDemo.Services
{
    public class MyScopedOtherService : IMyScopedService
    {
        public MyScopedOtherService()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }
    }
}
