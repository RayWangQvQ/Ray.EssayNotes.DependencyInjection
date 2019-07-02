using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc.Helpers
{
    public static class ReflectionHelper
    {
        /// <summary>
        ///  获取Core Web项目所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssembliesCoreWeb()
        {
            var assemblies = GetReferencedAssemblies().ToList();
            Assembly assembly = Assembly.GetEntryAssembly();
            assemblies.Add(assembly);
            return assemblies.ToArray();
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
