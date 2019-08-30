//系统包
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
//微软包
using Microsoft.Extensions.DependencyModel;

namespace Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc.Helpers
{
    /// <summary>
    /// 反射辅助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        ///  获取Asp.Net Core项目所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssembliesCoreWeb()
        {
            var list = new List<Assembly>();
            DependencyContext dependencyContext = DependencyContext.Default;
            IEnumerable<CompilationLibrary> libs = dependencyContext.CompileLibraries
                .Where(lib => !lib.Serviceable && lib.Type != "package" && lib.Name.StartsWith("Ray"));
            foreach (var lib in libs)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                list.Add(assembly);
            }
            return list.ToArray();
        }
    }
}
