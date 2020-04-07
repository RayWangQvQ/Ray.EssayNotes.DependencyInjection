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
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="myTransientService"></param>
        /// <param name="myScopedService"></param>
        /// <param name="mySingletonService"></param>
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
            Console.WriteLine($"单例1:{mySingletonService1.GetHashCode()}");
            Console.WriteLine($"单例2:{mySingletonService2.GetHashCode()}");

            Console.WriteLine($"瞬时1:{myTransientService1.GetHashCode()}");
            Console.WriteLine($"瞬时2:{myTransientService2.GetHashCode()}");

            Console.WriteLine($"域内单例1:{myScopedService1.GetHashCode()}");
            Console.WriteLine($"域内单例2:{myScopedService2.GetHashCode()}");

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}