using System;
using Ray.EssayNotes.Di.DiDemo.IServices;

namespace Ray.EssayNotes.Di.DiDemo.Services
{
    public class OtherScopedService : IMyScopedService
    {
        public OtherScopedService()
        {
            Console.WriteLine($"【Create Instance】{this}：{this.GetHashCode()}");
        }
    }
}
