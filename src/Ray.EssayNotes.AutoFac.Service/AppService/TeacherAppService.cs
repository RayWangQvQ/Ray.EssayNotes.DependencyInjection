//本地项目包
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.Service.AppService
{
    /// <summary>
    /// 教师逻辑处理
    /// </summary>
    public class TeacherAppService : ITeacherService
    {
        /// <summary>
        /// 用于属性注入
        /// </summary>
        public ITeacherRepository TeacherRepository { get; set; }

        public IStudentRepository StudentRepository { get; set; }

        public string TestStr { get; set; }

        public int TestInt;//不是属性，不可以属性注入

        public string GetTeacherName(long id)
        {
            return TeacherRepository?.Get(111).Name;
        }
    }
}
