using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Model;

namespace Ray.EssayNotes.AutoFac.Repository.IRepository
{
    public interface ITeacherRepository : IBaseRepository<TeacherEntity>
    {
        string GetName(long id);
    }
}
