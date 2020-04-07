using Ray.EssayNotes.AutoFac.Domain.Entity;

namespace Ray.EssayNotes.AutoFac.Domain.IRepository
{
    /// <summary>
    /// 学生仓储interface
    /// </summary>
    public interface IStudentRepository : IBaseRepository<StudentEntity>
    {
        string GetName(long id);
    }
}
