//系统包
using System.Linq;
//本地包
using Ray.EssayNotes.AutoFac.Domain.Entity;

namespace Ray.EssayNotes.AutoFac.Domain.IRepository
{
    /// <summary>
    /// 基类仓储interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T:BaseEntity
    {
        T Get(long id);
        IQueryable<T> GetAll();
        long Add(T entity);
        void Update(T entity);
        void Delete(long id);
    }
}
