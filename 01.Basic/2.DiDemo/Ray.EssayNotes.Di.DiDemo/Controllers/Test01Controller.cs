using System;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.Di.DiDemo.IServices;

namespace Ray.EssayNotes.Di.DiDemo.Controllers
{
    /// <summary>
    /// 测试构造注入
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test01Controller : ControllerBase
    {
        private readonly IMyTransientService _myTransientService1;
        private readonly IMyTransientService _myTransientService2;
        private readonly IMyScopedService _myScopedService1;
        private readonly IMyScopedService _myScopedService2;
        private readonly IMySingletonService _mySingletonService1;
        private readonly IMySingletonService _mySingletonService2;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="myTransientService"></param>
        /// <param name="myScopedService"></param>
        /// <param name="mySingletonService"></param>
        public Test01Controller(IMyTransientService myTransientService1,
            IMyTransientService myTransientService2,
            IMyScopedService myScopedService1,
            IMyScopedService myScopedService2,
            IMySingletonService mySingletonService1,
            IMySingletonService mySingletonService2)
        {
            _myTransientService1 = myTransientService1;
            _myTransientService2 = myTransientService2;
            _myScopedService1 = myScopedService1;
            _myScopedService2 = myScopedService2;
            _mySingletonService1 = mySingletonService1;
            _mySingletonService2 = mySingletonService2;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"单例1:{_mySingletonService1.GetHashCode()}");
            Console.WriteLine($"单例2:{_mySingletonService2.GetHashCode()}");

            Console.WriteLine($"瞬时1:{_myTransientService1.GetHashCode()}");
            Console.WriteLine($"瞬时2:{_myTransientService2.GetHashCode()}");

            Console.WriteLine($"域内单例1:{_myScopedService1.GetHashCode()}");
            Console.WriteLine($"域内单例2:{_myScopedService2.GetHashCode()}");

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}