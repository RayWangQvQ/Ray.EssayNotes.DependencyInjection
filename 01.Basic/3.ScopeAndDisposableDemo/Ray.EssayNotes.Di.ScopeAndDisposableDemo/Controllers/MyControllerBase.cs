using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.Di.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试瞬时实例的释放
    /// </summary>
    public abstract class MyControllerBase : ControllerBase
    {
        /// <summary>
        /// 打印根容器中持久化实例池与可释放实例池
        /// </summary>
        protected void PrintFromRootScope()
        {
            Console.WriteLine("\r\n\r\n根域中:");
            PrintFromScope(Startup.ServiceProviderRoot);
        }

        /// <summary>
        /// 打印请求域中持久化实例池与可释放实例池
        /// </summary>
        protected void PrintFromRequestServiceScope()
        {
            Console.Write("\r\n\r\n当前请求域中:");
            PrintFromScope(HttpContext.RequestServices);
        }

        /// <summary>
        /// 打印指定域中持久化实例池与可释放实例池
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected void PrintFromScope(IServiceProvider serviceProvider)
        {
            PrintInstancePool(serviceProvider);
            PrintDisposablePool(serviceProvider);
        }

        /// <summary>
        /// 打印容器中持久化实例池
        /// </summary>
        /// <param name="serviceProvider"></param>
        private void PrintInstancePool(IServiceProvider serviceProvider)
        {
            Console.Write("持久化实例池内容：");
            var dic = serviceProvider.GetResolvedServicesFromScope()
                .Select(x => x.ToString());
            Console.WriteLine(JsonConvert.SerializeObject(dic).AsFormatJsonString());
        }

        /// <summary>
        /// 打印容器中可释放实例池
        /// </summary>
        /// <param name="serviceProvider"></param>
        private void PrintDisposablePool(IServiceProvider serviceProvider)
        {
            Console.Write("可释放实例池内容：");
            var list = serviceProvider.GetDisposablesFromScope()
                .Select(x => x.ToString());
            Console.WriteLine(JsonConvert.SerializeObject(list).AsFormatJsonString());
        }
    }
}