using Ray.EssayNotes.AutoFac.Model;
using Ray.EssayNotes.AutoFac.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.Repository.Repository
{
    public class StudentRepository : BaseRepository<StudentEntity>, IStudentRepository
    {
        public string GetName(long id)
        {
            return "学生张三";
        }
    }
}
