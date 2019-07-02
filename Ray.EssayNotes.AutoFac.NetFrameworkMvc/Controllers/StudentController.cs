using Ray.EssayNotes.AutoFac.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ray.EssayNotes.AutoFac.NetFrameworkMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetStuNameById(long id)
        {
            return _studentService.GetStuName(id);
        }
    }
}