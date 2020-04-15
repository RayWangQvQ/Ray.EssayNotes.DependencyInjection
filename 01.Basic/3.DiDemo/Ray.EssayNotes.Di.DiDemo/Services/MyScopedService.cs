using System;
using Ray.EssayNotes.Di.DiDemo.IServices;

namespace Ray.EssayNotes.Di.DiDemo.Services
{
    public class MyScopedService : IMyScopedService
    {
        public MyScopedService()
        {
            Console.WriteLine($"【Create Instance】{this}：{this.GetHashCode()}");
        }
    }
}
