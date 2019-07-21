using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.AutoFac.Repository;
using Ray.EssayNotes.AutoFac.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.CoreApi.Controllers
{
    public class TestInstancePreRequestController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;
        private readonly ITestInstancePreRequestService _testInstancePreRequestService;

        public TestInstancePreRequestController(MyDbContext myDbContext, 
            ITestInstancePreRequestService testInstancePreRequestService)
        {
            _myDbContext = myDbContext;
            _testInstancePreRequestService = testInstancePreRequestService;
        }

        [Route("TestInstancePreRequest/Test")]
        public string Test()
        {
            string re = "";

            re = $"控制器：{_myDbContext.Tag}\r\n";
            string serviceRe = _testInstancePreRequestService.Test();
            re += $"Service：{serviceRe}";

            return re;
        }
    }
}
