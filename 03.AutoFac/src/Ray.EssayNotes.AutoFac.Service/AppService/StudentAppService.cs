//本地项目包
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.Service.AppService
{
    /// <summary>
    /// 学生逻辑处理
    /// </summary>
    public class StudentAppService : IStudentAppService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentAppService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public StudentAppService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public StudentAppService(StudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
        }

        public StudentAppService(StudentRepository studentRepository, ITeacherRepository teacherRepository, string test)
        {
            _studentRepository = studentRepository;
        }

        public ITeacherRepository TeacherRepository { get; set; }

        public string GetStuName(long id)
        {
            var stu = _studentRepository.Get(id);
            return stu.Name;
        }
    }
}
