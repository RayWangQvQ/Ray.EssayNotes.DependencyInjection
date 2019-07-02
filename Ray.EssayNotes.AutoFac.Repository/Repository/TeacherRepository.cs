using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Model;
using Ray.EssayNotes.AutoFac.Repository.IRepository;

namespace Ray.EssayNotes.AutoFac.Repository.Repository
{
    public class TeacherRepository : BaseRepository<TeacherEntity>, ITeacherRepository
    {
        public string GetName(long id)
        {
            return "教师李四";
        }
    }
}
