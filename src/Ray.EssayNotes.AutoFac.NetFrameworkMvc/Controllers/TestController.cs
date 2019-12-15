using Ray.EssayNotes.AutoFac.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ray.EssayNotes.AutoFac.NetFrameworkMvc.Controllers
{
    public class TestController:Controller
    {
        private readonly ITestService _testService;
        private readonly ITest2Service _test2Service;

        public TestController(ITestService testService,
            ITest2Service test2Service)
        {
            _testService = testService;
            _test2Service = test2Service;
        }

        [Route("Test/Test")]
        public string Test()
        {
            string re = "";

            re += $"第一个：{_testService.Test()}";
            re += "\r\n";
            re += $"第二个：{_test2Service.Test()}";

            return re;
        }
    }
}