using Ray.EssayNotes.AutoFac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.Repository.IRepository
{
    public interface IStudentRepository : IBaseRepository<StudentEntity>
    {
        string GetName(long id);
    }
}
