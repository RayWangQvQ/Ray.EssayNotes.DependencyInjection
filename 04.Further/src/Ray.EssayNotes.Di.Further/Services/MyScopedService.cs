using System;
using System.Linq;
using Ray.EssayNotes.Di.ContainerDemo.IServices;

namespace Ray.EssayNotes.Di.ContainerDemo.Services
{
    public class MyScopedService : IMyScopedService
    {
        public MyScopedService()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }
    }
}
