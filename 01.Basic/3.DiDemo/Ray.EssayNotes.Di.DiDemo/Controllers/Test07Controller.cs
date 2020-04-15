using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.Di.DiDemo.IServices;

namespace Ray.EssayNotes.Di.DiDemo.Controllers
{
    /// <summary>
    /// 尝试注册多实现服务
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test07Controller : ControllerBase
    {
        private readonly IMyTransientService _myTransientService1;
        private readonly IMyTransientService _myTransientService2;
        private readonly IMySingletonService _mySingletonService1;
        private readonly IMySingletonService _mySingletonService2;

        private readonly IEnumerable<IMyScopedService> _myScopedServices;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="myTransientService"></param>
        /// <param name="myScopedService"></param>
        /// <param name="mySingletonService"></param>
        public Test07Controller(IMyTransientService myTransientService1,
            IMyTransientService myTransientService2,
            IMySingletonService mySingletonService1,
            IMySingletonService mySingletonService2,
            IEnumerable<IMyScopedService> myScopedServices)//可以注入集合
        {
            _myTransientService1 = myTransientService1;
            _myTransientService2 = myTransientService2;
            _mySingletonService1 = mySingletonService1;
            _mySingletonService2 = mySingletonService2;

            _myScopedServices = myScopedServices;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"单例1:{_mySingletonService1.GetHashCode()}");
            Console.WriteLine($"单例2:{_mySingletonService2.GetHashCode()}");

            Console.WriteLine($"瞬时1:{_myTransientService1.GetHashCode()}");
            Console.WriteLine($"瞬时2:{_myTransientService2.GetHashCode()}");

            foreach (var item in _myScopedServices)
            {
                Console.WriteLine($"域内单例:{item.GetType()}:{item.GetHashCode()}");
            }

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}