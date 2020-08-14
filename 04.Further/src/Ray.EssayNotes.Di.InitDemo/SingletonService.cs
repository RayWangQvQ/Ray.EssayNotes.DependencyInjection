using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ray.EssayNotes.Di.InitDemo
{
    public class SingletonService
    {
        public SingletonService(IServiceProvider serviceProvider)
        {
            Console.WriteLine($"全局单例中注入的容器：{serviceProvider.GetHashCode()}");
        }
    }
}
