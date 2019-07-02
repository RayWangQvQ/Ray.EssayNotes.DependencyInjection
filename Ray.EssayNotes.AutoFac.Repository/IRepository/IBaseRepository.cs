using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Model;

namespace Ray.EssayNotes.AutoFac.Repository.IRepository
{
    public interface IBaseRepository<T> where T:BaseEntity
    {
        T Get(long id);
        IQueryable<T> GetAll();
        long Add(T entity);
        void Update(T entity);
        void Delete(long id);
    }
}
