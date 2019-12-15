using Ray.EssayNotes.AutoFac.Repository;
using Ray.EssayNotes.AutoFac.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.Service.Service
{
    public class TestService : ITestService
    {
        private readonly MyDbContext _myDbContext;

        public TestService(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public string Test()
        {
            return _myDbContext.Tag.ToString();
        }
    }
}
