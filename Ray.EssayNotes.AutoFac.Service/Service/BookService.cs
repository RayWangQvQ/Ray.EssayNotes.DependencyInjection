using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Model;
using Ray.EssayNotes.AutoFac.Repository.IRepository;
using Ray.EssayNotes.AutoFac.Service.IService;

namespace Ray.EssayNotes.AutoFac.Service.Service
{
    public class BookService : IBookService
    {
        IBaseRepository<BookEntity> _bookRepository;
        public BookService(IBaseRepository<BookEntity> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public string GetTitle(long id)
        {
            return _bookRepository.Get(id).Title;
        }
    }
}
