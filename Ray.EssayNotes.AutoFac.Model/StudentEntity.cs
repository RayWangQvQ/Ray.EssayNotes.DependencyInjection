using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.Model
{
    public class StudentEntity:BaseEntity
    {
        public string Name { get; set; }

        public int Grade { get; set; }
        
    }
}
