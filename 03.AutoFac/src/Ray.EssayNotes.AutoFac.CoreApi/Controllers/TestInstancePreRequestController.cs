using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.CoreApi.Controllers
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TestInstancePreRequestController : ControllerBase
    {
        private readonly MyDbContext _myDbContext1;
        private readonly MyDbContext _myDbContext2;

        public TestInstancePreRequestController(MyDbContext myDbContext1, MyDbContext myDbContext2)
        {
            _myDbContext1 = myDbContext1;
            _myDbContext2 = myDbContext2;
        }

        [HttpGet]
        public bool Test()
        {
            Console.WriteLine($"_myDbContext1：{_myDbContext1.GetHashCode()}");
            Console.WriteLine($"_myDbContext2：{_myDbContext2.GetHashCode()}");

            using (var scope = HttpContext.RequestServices.CreateScope())
            {
                var instance1 = scope.ServiceProvider.GetService<MyDbContext>();
                Console.WriteLine($"instance1：{instance1.GetHashCode()}");
            }

            using (var scope1 = MyContainer.Root.BeginLifetimeScope())
            {
                using (var scope2 = scope1.BeginLifetimeScope(Autofac.Core.Lifetime.MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
                {
                    var instance2 = scope2.Resolve<MyDbContext>();
                    Console.WriteLine($"instance2：{instance2.GetHashCode()}");
                }
            }

            return true;
        }
    }
}
