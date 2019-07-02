using Ray.EssayNotes.AutoFac.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ray.EssayNotes.AutoFac.Model;

namespace Ray.EssayNotes.AutoFac.Repository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public virtual T Get(long id)
        {
            T instance = Activator.CreateInstance<T>();

            var stuEntity = instance as StudentEntity;
            if (stuEntity != null)
            {
                stuEntity.Id = id;
                stuEntity.Name = "学生张三";
                stuEntity.Grade = 99;
                return stuEntity as T;
            }

            var teacherEntity = instance as TeacherEntity;
            if (teacherEntity != null)
            {
                teacherEntity.Id = id;
                teacherEntity.Name = "教师李四";
                teacherEntity.Salary = "10K";
                return teacherEntity as T;
            }

            var bookEntity = instance as BookEntity;
            if (bookEntity != null)
            {
                bookEntity.Id = id;
                bookEntity.Title = "《百年孤独》";
                bookEntity.Writer = "加西亚马尔克斯";
                return bookEntity as T;
            }
            return instance;
        }

        public virtual IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual long Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
