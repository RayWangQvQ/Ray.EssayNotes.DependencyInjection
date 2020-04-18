using System;
using System.Text.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.Further.Extensions;

namespace Ray.EssayNotes.Di.Further.Test
{
    [Description("根容器下的域工厂")]
    public class Test04 : TestBase
    {
        public override void Print()
        {
            var factory = Program.ServiceProviderRoot.GetRequiredService<IServiceScopeFactory>();
            Console.WriteLine($"工厂：{factory.GetType()}({factory.GetHashCode()})");
        }
    }
}
