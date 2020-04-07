using System;

namespace Ray.EssayNotes.Di.DiDemo.Dtos
{
    public class OtherDto
    {
        public OtherDto()
        {
            Console.WriteLine($"【Create Instance】{this}：{this.GetHashCode()}");
        }

        public MyDto MyDto { get; set; }
    }
}
