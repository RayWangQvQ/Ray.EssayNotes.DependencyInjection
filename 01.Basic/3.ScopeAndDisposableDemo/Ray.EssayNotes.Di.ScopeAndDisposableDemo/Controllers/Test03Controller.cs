using System;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.Di.ScopeAndDisposableDemo.IServices;

namespace Ray.EssayNotes.Di.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试全局单例
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test03Controller : MyControllerBase
    {
        private readonly IOrderService _orderService1;
        private readonly IOrderService _orderService2;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="orderService1"></param>
        /// <param name="orderService2"></param>
        public Test03Controller(IOrderService orderService1, IOrderService orderService2)
        {
            this._orderService1 = orderService1;
            this._orderService2 = orderService2;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_orderService1:{_orderService1.GetHashCode()}");
            Console.WriteLine($"_orderService2:{_orderService2.GetHashCode()}");

            this.PrintFromRootScope();
            this.PrintFromRequestServiceScope();

            Console.WriteLine($"========请求结束=======");

            /* 请求结束，但是程序没有停止，这里只会释放请求域，并不会释放根域
             * 所以这里不会去释放全局单例的实例
             */

            return true;
        }
    }
}