using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.Service.Dtos
{
    public class DtoToken
    {
        public DtoToken()
        {
            this.Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }
    }
}
