//本地项目包
using Ray.EssayNotes.AutoFac.Domain.Entity;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Service.IAppService;

namespace Ray.EssayNotes.AutoFac.Service.AppService
{
    /// <summary>
    /// 书籍逻辑处理
    /// </summary>
    public class BookAppService : IBookService
    {
        IBaseRepository<BookEntity> _bookRepository;
        public BookAppService(IBaseRepository<BookEntity> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public string GetTitle(long id)
        {
            return _bookRepository.Get(id).Title;
        }
    }
}
