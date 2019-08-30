//系统包
using System.Web.Http;
//本地项目包
using Ray.EssayNotes.AutoFac.Service.IService;

namespace Ray.EssayNotes.AutoFac.NetFrameworkApi.Controllers
{
    /// <summary>
    /// 学生模块Api
    /// </summary>
    public class StudentController : ApiController
    {
        private readonly IStudentService _studentService;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="studentService"></param>
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 获取学生姓名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Student/GetStuNameById")]
        public string GetStuNameById(long id)
        {
            return _studentService.GetStuName(123);
        }
    }
}