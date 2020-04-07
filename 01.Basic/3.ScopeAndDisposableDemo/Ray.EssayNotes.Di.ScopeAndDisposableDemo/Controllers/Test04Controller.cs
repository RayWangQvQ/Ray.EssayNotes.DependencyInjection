using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Ray.EssayNotes.Di.ScopeAndDisposableDemo.IServices;

namespace Ray.EssayNotes.Di.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试单例的释放02
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test04Controller : MyControllerBase
    {
        private readonly IOrderService _orderService1;
        private readonly IOrderService _orderService2;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="orderService1"></param>
        /// <param name="orderService2"></param>
        public Test04Controller(IOrderService orderService1, IOrderService orderService2, IHostApplicationLifetime hostApplicationLifetime)
        {
            this._orderService1 = orderService1;
            this._orderService2 = orderService2;
            this._hostApplicationLifetime = hostApplicationLifetime;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_orderService1:{_orderService1.GetHashCode()}");
            Console.WriteLine($"_orderService2:{_orderService2.GetHashCode()}");

            this.PrintFromRootScope();

            Console.WriteLine($"========请求结束=======");
            _hostApplicationLifetime.StopApplication();//使整个应用停止

            /* 停止后会释放根容器，此时会释放单例对象
             */

            return true;
        }
    }
}