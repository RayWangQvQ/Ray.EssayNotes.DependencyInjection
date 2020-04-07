using System;

namespace Ray.EssayNotes.Di.DiDemo.Dtos
{
    public class GenericDto<T>
    {
        public GenericDto()
        {
            Console.WriteLine($"【Create Instance】{this}：{this.GetHashCode()}");
        }
    }
}
