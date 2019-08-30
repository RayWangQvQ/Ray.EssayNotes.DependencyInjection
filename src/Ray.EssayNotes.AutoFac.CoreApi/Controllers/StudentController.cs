//系统包
using Microsoft.AspNetCore.Mvc;
//本地三方包
using Ray.EssayNotes.AutoFac.Service.IService;

namespace Ray.EssayNotes.AutoFac.CoreApi.Controllers
{
    /// <summary>
    /// 学生模块接口
    /// </summary>
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Route("Student/GetStuNameById")]
        public string GetStuNameById(long id)
        {
            return _studentService.GetStuName(id);
        }
    }
}