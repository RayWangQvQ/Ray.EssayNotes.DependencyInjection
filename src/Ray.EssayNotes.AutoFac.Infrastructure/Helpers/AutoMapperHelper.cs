using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Helpers
{
    public class AutoMapperHelper
    {
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TTarget>());
            var mapper = config.CreateMapper();
            TTarget info = mapper.Map<TSource, TTarget>(source);
            return info;
        }
    }
}
