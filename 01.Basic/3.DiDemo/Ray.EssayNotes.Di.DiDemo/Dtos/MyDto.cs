using System;

namespace Ray.EssayNotes.Di.DiDemo.Dtos
{
    public class MyDto
    {
        public MyDto()
        {
            Console.WriteLine($"【Create Instance】{this}：{this.GetHashCode()}");
        }
    }
}
