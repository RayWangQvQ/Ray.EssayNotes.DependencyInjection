using System;
using System.Linq;
using Ray.EssayNotes.Di.ContainerDemo.IServices;

namespace Ray.EssayNotes.Di.ContainerDemo.Services
{
    public class MySingletonService : IMySingletonService
    {
        public MySingletonService()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }
    }
}
