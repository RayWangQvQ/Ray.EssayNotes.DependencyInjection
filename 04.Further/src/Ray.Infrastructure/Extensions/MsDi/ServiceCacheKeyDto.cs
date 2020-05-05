using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Extensions.MsDi
{
    public struct ServiceCacheKeyDto
    {
        public Type Type { get; set; }
        public int Slot { get; set; }
    }
}
