using System;
using Ray.EssayNotes.Di.ScopeAndDisposableDemo.IServices;

namespace Ray.EssayNotes.Di.ScopeAndDisposableDemo.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        public OrderService()
        {
            Console.WriteLine($"【Create Instance】{this}：{this.GetHashCode()}");
        }

        public void Dispose()
        {
            Console.WriteLine($"【Dispose】{this}：{this.GetHashCode()}");
        }
    }
}
