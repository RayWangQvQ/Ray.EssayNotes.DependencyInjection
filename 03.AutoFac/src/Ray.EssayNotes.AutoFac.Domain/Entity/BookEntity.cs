
namespace Ray.EssayNotes.AutoFac.Domain.Entity
{
    /// <summary>
    /// 书籍实体
    /// </summary>
    public class BookEntity : BaseEntity
    {
        /// <summary>
        /// 书名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Writer { get; set; }
    }
}
