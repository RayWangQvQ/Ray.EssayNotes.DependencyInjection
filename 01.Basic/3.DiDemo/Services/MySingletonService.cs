using System;
using Ray.EssayNotes.Di.DiDemo.IServices;

namespace Ray.EssayNotes.Di.DiDemo.Services
{
    public class MySingletonService : IMySingletonService
    {
        public MySingletonService()
        {
            Console.WriteLine($"【Create Instance】{this}：{this.GetHashCode()}");
        }
    }
}
