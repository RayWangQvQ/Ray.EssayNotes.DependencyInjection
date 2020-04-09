using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    public class TestLifetimeScopeFactory : TestFactory
    {
        public override Type TestBaseType => typeof(TestLifetimeScopeBase);
    }
}
