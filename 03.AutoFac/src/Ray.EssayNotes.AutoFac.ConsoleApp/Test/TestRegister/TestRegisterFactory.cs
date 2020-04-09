using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegisterFactory : TestFactory
    {
        public override Type TestBaseType => typeof(TestRegisterBase);
    }
}
