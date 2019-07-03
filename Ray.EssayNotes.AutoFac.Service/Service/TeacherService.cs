using Ray.EssayNotes.AutoFac.Repository.IRepository;
using Ray.EssayNotes.AutoFac.Service.IService;


namespace Ray.EssayNotes.AutoFac.Service.Service
{
    /// <summary>
    /// 教师逻辑处理
    /// </summary>
    public class TeacherService : ITeacherService
    {
        public ITeacherRepository TeacherRepository { get; set; }

        public string GetTeacherName(long id)
        {
            return TeacherRepository?.Get(111).Name;
        }
    }
}
