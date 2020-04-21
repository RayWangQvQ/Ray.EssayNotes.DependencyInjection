using System;
using System.Linq;
using Ray.EssayNotes.Di.ContainerDemo.IServices;

namespace Ray.EssayNotes.Di.ContainerDemo.Services
{
    public class MyScopedOtherService : IMyScopedService
    {
        public MyScopedOtherService()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }
    }
}
