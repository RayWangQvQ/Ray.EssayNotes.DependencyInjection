using System;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.Di.DiDemo.Dtos;

namespace Ray.EssayNotes.Di.DiDemo.Controllers
{
    /// <summary>
    /// 测试new实例注册
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test03Controller : ControllerBase
    {
        private readonly MyDto _myDto1;
        private readonly MyDto _myDto2;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="myDto1"></param>
        /// <param name="myDto2"></param>
        public Test03Controller(MyDto myDto1,
            MyDto myDto2)
        {
            _myDto1 = myDto1;
            _myDto2 = myDto2;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_myDto1:{_myDto1.GetHashCode()}");
            Console.WriteLine($"_myDto2:{_myDto2.GetHashCode()}");

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}