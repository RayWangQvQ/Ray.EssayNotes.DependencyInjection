//本地项目包
using Ray.EssayNotes.AutoFac.Model;

namespace Ray.EssayNotes.AutoFac.Repository.IRepository
{
    /// <summary>
    /// 教师仓储interface
    /// </summary>
    public interface ITeacherRepository : IBaseRepository<TeacherEntity>
    {
        string GetName(long id);
    }
}
