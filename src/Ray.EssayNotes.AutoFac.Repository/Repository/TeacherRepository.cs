//本地项目包
using Ray.EssayNotes.AutoFac.Domain.Entity;
using Ray.EssayNotes.AutoFac.Domain.IRepository;

namespace Ray.EssayNotes.AutoFac.Repository.Repository
{
    /// <summary>
    /// 教师仓储
    /// </summary>
    public class TeacherRepository : BaseRepository<TeacherEntity>, ITeacherRepository
    {
        public string GetName(long id)
        {
            return "教师李四";
        }
    }
}
