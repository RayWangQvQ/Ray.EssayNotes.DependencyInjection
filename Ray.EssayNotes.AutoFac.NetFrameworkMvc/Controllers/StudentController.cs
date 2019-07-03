using System.Web.Mvc;
//
using Ray.EssayNotes.AutoFac.Service.IService;


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
        /// 获取学生姓名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetStuNameById(long id)
        {
            return _studentService.GetStuName(id);
        }
    }
}