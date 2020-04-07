using System;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.Di.ScopeAndDisposableDemo.IServices;

namespace Ray.EssayNotes.Di.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试瞬时实例的释放
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test01Controller : MyControllerBase
    {
        private readonly IOrderService _orderService1;
        private readonly IOrderService _orderService2;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="orderService1"></param>
        /// <param name="orderService2"></param>
        public Test01Controller(IOrderService orderService1, IOrderService orderService2)
        {
            this._orderService1 = orderService1;
            this._orderService2 = orderService2;

            //不能在控制器的构造函数下打印，因为此时的HttpContext还没有生成
            //this.PrintFromRequestServiceScope();
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_orderService1:{_orderService1.GetHashCode()}");
            Console.WriteLine($"_orderService2:{_orderService2.GetHashCode()}");

            //打印下持久化实例池、可释放实例池
            this.PrintFromRequestServiceScope();

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}