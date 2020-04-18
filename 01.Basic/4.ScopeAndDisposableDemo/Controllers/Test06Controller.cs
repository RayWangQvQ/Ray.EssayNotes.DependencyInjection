using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ray.EssayNotes.Di.ScopeAndDisposableDemo.IServices;

namespace Ray.EssayNotes.Di.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 根域释放，是否会释放子域
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test06Controller : MyControllerBase
    {
        private readonly IOrderService _orderService1;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="orderService1"></param>
        /// <param name="orderService2"></param>
        /// <param name="hostApplicationLifetime"></param>
        public Test06Controller(IOrderService orderService1,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            this._orderService1 = orderService1;
            this._hostApplicationLifetime = hostApplicationLifetime;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"请求域注入对象：{_orderService1.GetHashCode()}");

            var childScope = Startup.ServiceProviderRoot.CreateScope();//这里不使用using，因为using相当于try，不管发不发生生异常，都能确保最后的释放
            var instance2 = childScope.ServiceProvider.GetService<IOrderService>();
            Console.WriteLine($"子域注入对象：{instance2.GetHashCode()}");

            _hostApplicationLifetime.StopApplication();//在子域释放前停止整个程序

            return true;
        }
    }
}