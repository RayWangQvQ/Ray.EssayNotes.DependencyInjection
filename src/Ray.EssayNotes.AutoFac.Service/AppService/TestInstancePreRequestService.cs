//本地项目包
using Ray.EssayNotes.AutoFac.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.Service.AppService
{
    public class TestInstancePreRequestService : ITestInstancePreRequestService
    {
        private readonly MyDbContext _myDbContext;

        public TestInstancePreRequestService(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public string Test()
        {
            return _myDbContext.Tag.ToString();
        }
    }
}
