using System;
using System.Linq;
using Ray.EssayNotes.Di.ContainerDemo.IServices;

namespace Ray.EssayNotes.Di.ContainerDemo.Services
{
    public class MyTransientService : IMyTransientService
    {
        public MyTransientService()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}：{this.GetHashCode()}");
        }
    }
}
