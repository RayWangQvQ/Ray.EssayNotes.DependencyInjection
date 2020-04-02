using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test
{
    /// <summary>
    /// 抽象工厂
    /// </summary>
    public interface ITestFactory
    {
        ITest Create(string num);

        string TestType { get; }

        string GetSelectionRange { get; }
    }
}
