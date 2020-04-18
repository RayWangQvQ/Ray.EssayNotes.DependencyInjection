using System;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.Di.DiDemo.Dtos;

namespace Ray.EssayNotes.Di.DiDemo.Controllers
{
    /// <summary>
    /// 利用注册委托实现属性注入
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test05Controller : ControllerBase
    {
        private readonly OtherDto _otherDto;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="otherDto"></param>
        public Test05Controller(OtherDto otherDto)
        {
            _otherDto = otherDto;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_otherDto:{_otherDto.GetHashCode()}");
            Console.WriteLine($"_otherDto.MyDto:{_otherDto.MyDto.GetHashCode()}");

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}