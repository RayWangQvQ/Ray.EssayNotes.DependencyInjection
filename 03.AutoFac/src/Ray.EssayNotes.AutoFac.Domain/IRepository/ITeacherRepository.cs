using Ray.EssayNotes.AutoFac.Domain.Entity;

namespace Ray.EssayNotes.AutoFac.Domain.IRepository
{
    /// <summary>
    /// 教师仓储interface
    /// </summary>
    public interface ITeacherRepository : IBaseRepository<TeacherEntity>
    {
        string GetName(long id);
    }
}
