using System;
using Ray.EssayNotes.Di.DiDemo.IServices;

namespace Ray.EssayNotes.Di.DiDemo.Services
{
    public class MyTransientService : IMyTransientService
    {
        public MyTransientService()
        {
            Console.WriteLine($"【Create Instance】{this}：{this.GetHashCode()}");
        }
    }
}
