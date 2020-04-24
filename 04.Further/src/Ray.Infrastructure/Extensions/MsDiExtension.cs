using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Microsoft.CSharp;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class MsDiExtension
    {
        /// <summary>
        /// 获取容器引擎对象
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>ServiceProviderEngine对象</returns>
        public static object GetEngine(this IServiceProvider serviceProvider)
        {
            //因为容器、引擎、引擎域都实现了容器接口（IServiceProvider），所以要分情况
            if (serviceProvider is ServiceProvider)//根容器
            {
                return serviceProvider.GetFieldValue("_engine");
            }
            if (serviceProvider.GetType().Name.EndsWith("ServiceProviderEngine"))//引擎
            {
                return serviceProvider;
            }
            if (serviceProvider.GetType().Name.Contains("ServiceProviderEngineScope"))//引擎域
            {
                return serviceProvider.GetPropertyValue("Engine");
            }
            return null;
        }

        /// <summary>
        /// 获取根域对象
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>代表根域（根IServiceScope）的ServiceProviderEngineScope对象</returns>
        public static object GetRootScope(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetEngine()
                .GetPropertyValue("RootScope");
            /**
             * 引擎对象是唯一的，且引擎对象的RootScope属性指向根域（根引擎域）
             * ServiceProviderEngineScope对象为internal访问权限，所以返回object
             */
        }

        /// <summary>
        /// 获取容器内的实例池中已持久化的实例集合
        /// </summary>
        /// <param name="serviceProvider">容器</param>
        /// <returns></returns>
        public static IEnumerable<object> GetResolvedServicesFromScope(this IServiceProvider serviceProvider)
        {
            var objList = serviceProvider
                    .GetRequiredService<IServiceProvider>()//引擎域
                    .GetPropertyValue("ResolvedServices")//返回Dictionary<Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceCacheKey,object>的装箱后的object，注意ServiceCacheKey为internal struct
                    .GetPropertyValue("Values")
                as IEnumerable<object>;

            return objList ?? new List<object>();
        }

        /// <summary>
        /// 获取容器内的可释放实例池中的实例集合
        /// </summary>
        /// <param name="serviceProvider">容器</param>
        /// <returns></returns>
        public static IEnumerable<object> GetDisposablesFromScope(this IServiceProvider serviceProvider)
        {
            var objList = serviceProvider
                    .GetRequiredService<IServiceProvider>()//引擎域
                    .GetFieldValue("_disposables")
                as IEnumerable<object>;

            return objList ?? new List<object>();
        }
    }
}
