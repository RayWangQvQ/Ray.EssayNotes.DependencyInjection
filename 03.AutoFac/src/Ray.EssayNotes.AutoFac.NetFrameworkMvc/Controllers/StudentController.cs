//系统包
using System.Web.Mvc;
//本地项目包
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.NetFrameworkMvc.Controllers
{
    /// <summary>
    /// 学生模块Api
    /// </summary>
    public class StudentController : Controller
    {
        private readonly IStudentAppService _studentService;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="studentService"></param>
        public StudentController(IStudentAppService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 获取学生姓名
        /// </summary>
        /// <returns></returns>
        public string GetStuNameById()
        {
            return _studentService.GetStuName(123);
        }
    }
}