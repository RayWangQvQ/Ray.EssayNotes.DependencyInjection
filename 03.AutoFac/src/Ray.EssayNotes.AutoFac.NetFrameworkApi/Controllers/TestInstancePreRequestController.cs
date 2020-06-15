//系统包
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using Autofac;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository;
//本地项目包
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.NetFrameworkApi.Controllers
{
    /// <summary>
    /// 学生模块Api
    /// </summary>
    public class TestInstancePreRequestController : ApiController
    {
        private readonly MyDbContext _myDbContext1;
        private readonly MyDbContext _myDbContext2;

        public TestInstancePreRequestController(MyDbContext myDbContext1, MyDbContext myDbContext2)
        {
            _myDbContext1 = myDbContext1;
            _myDbContext2 = myDbContext2;
        }

        [HttpGet]
        [Route("TestInstancePreRequest/Test")]
        public Dictionary<string, int> Test()
        {
            var dic = new Dictionary<string, int>
            {
                {"_myDbContext1", _myDbContext1.GetHashCode()},
                {"_myDbContext2", _myDbContext2.GetHashCode()},
            };

            using (var scope = RayContainer.AutofacRootScope.BeginLifetimeScope(Autofac.Core.Lifetime.MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
            {
                var instance = scope.Resolve<MyDbContext>();
                dic.Add("instance1", instance.GetHashCode());
            }

            using (var scope = RayContainer.AutofacRootScope.BeginLifetimeScope())
            {
                var instance = scope.Resolve<MyDbContext>();
                dic.Add("instance2", instance.GetHashCode());
            }

            return dic;
        }
    }
}