using System;
using System.Collections.Generic;
using System.Text;
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.Service.AppService
{
    public class MyOtherService : IMyService
    {
        public MyOtherService()
        {
            Console.WriteLine($"Create Instance of MyOtherService：{this.GetHashCode()}");
        }
    }
}
