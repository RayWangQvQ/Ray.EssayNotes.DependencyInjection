using System;
using System.Collections.Generic;
using System.Text;
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.Service.AppService
{
    public class MyService : IMyService
    {
        public MyService()
        {
            Console.WriteLine($"Create Instance of MyService：{this.GetHashCode()}");
        }
    }
}
