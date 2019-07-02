using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Repository.IRepository;
using Ray.EssayNotes.AutoFac.Service.IService;

namespace Ray.EssayNotes.AutoFac.Service.Service
{
    public class TeacherService : ITeacherService
    {
        public ITeacherRepository TeacherRepository { get; set; }

        public string GetTeacherName(long id)
        {
            return TeacherRepository?.Get(111).Name;
        }
    }
}
