using System;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.Di.DiDemo.IServices;

namespace Ray.EssayNotes.Di.DiDemo.Controllers
{
    /// <summary>
    /// 测试参数注入
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test02Controller : ControllerBase
    {

        public Test02Controller()
        {

        }

        [HttpGet]
        public bool Get([FromServices]IMyTransientService myTransientService1,
            [FromServices]IMyTransientService myTransientService2,
            [FromServices]IMyScopedService myScopedService1,
            [FromServices]IMyScopedService myScopedService2,
            [FromServices]IMySingletonService mySingletonService1,
            [FromServices]IMySingletonService mySingletonService2)
        {
            Console.WriteLine($"【瞬时实例】myTransientService1:{myTransientService1.GetHashCode()}");
            Console.WriteLine($"【瞬时实例】myTransientService2:{myTransientService2.GetHashCode()}");

            Console.WriteLine($"【域内单例】myScopedService1:{myScopedService1.GetHashCode()}");
            Console.WriteLine($"【域内单例】myScopedService2:{myScopedService2.GetHashCode()}");

            Console.WriteLine($"【全局单例】mySingletonService1:{mySingletonService1.GetHashCode()}");
            Console.WriteLine($"【全局单例】mySingletonService2:{mySingletonService2.GetHashCode()}");

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}