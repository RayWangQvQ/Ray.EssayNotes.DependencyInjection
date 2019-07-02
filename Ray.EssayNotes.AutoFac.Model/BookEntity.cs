using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.Model
{
    public class BookEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Writer { get; set; }
    }
}
