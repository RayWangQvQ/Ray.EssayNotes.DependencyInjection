using Ray.EssayNotes.AutoFac.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Ray.EssayNotes.AutoFac.NetFrameworkApi.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("Student/GetStuNameById")]
        public string GetStuNameById(long id)
        {
            return _studentService.GetStuName(123);
        }
    }
}