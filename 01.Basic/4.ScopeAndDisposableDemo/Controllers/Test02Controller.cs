using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.ScopeAndDisposableDemo.IServices;

namespace Ray.EssayNotes.Di.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试域内单例实例释放
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test02Controller : MyControllerBase
    {
        private readonly IOrderService _orderService1;
        private readonly IOrderService _orderService2;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="orderService1"></param>
        /// <param name="orderService2"></param>
        public Test02Controller(IOrderService orderService1, IOrderService orderService2)
        {
            this._orderService1 = orderService1;
            this._orderService2 = orderService2;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_orderService1:{_orderService1.GetHashCode()}");
            Console.WriteLine($"_orderService2:{_orderService2.GetHashCode()}");

            using (var childScope = HttpContext.RequestServices.CreateScope())//RequestServices是当前请求所在的作用域
            {
                var orderService = childScope.ServiceProvider.GetService<IOrderService>();
                Console.WriteLine($"orderService:{orderService.GetHashCode()}");
            }

            //this.PrintFromRequestServiceScope();

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}