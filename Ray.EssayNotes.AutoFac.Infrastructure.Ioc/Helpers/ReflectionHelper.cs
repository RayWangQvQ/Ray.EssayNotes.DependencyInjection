using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Helpers
{
    public static class ReflectionHelper
    {
        /// <summary>
        ///  获取FrameWork Web项目所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssembliesWeb()
        {
            Assembly[] assemblies = System.Web.Compilation.BuildManager
                .GetReferencedAssemblies()
                .Cast<Assembly>()
                .Where(x => x.FullName.Contains("Ray"))
                .ToArray();
            return assemblies;
        }

        /// <summary>
        /// 获取当前程序集所有引用程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetReferencedAssemblies()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();//当前程序集
            Assembly[] allAssemblies = executingAssembly.GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Where(m => m.FullName.Contains("Ray"))
                .ToArray();

            return allAssemblies;
        }
    }
}
