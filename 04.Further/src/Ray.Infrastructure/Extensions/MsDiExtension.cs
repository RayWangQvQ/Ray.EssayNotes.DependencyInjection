﻿using System;
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
        /// 容器中可释放的
        /// </summary>
        private const string DisposableFiledName = "_disposables";

        private const string ResolvedServicesPropertyName = "ResolvedServices";

        /// <summary>
        /// 获取容器内的可释放实例池中的实例集合
        /// </summary>
        /// <param name="serviceProvider">容器</param>
        /// <returns></returns>
        public static IEnumerable<object> GetDisposablesFromScope(this IServiceProvider serviceProvider)
        {
            var objList = serviceProvider.GetFieldValue(DisposableFiledName)
                as IEnumerable<object>;

            return objList ?? new List<object>();
        }

        /// <summary>
        /// 获取容器内的实例池中已持久化的实例集合
        /// </summary>
        /// <param name="serviceProvider">容器</param>
        /// <returns></returns>
        public static IEnumerable<object> GetResolvedServicesFromScope(this IServiceProvider serviceProvider)
        {
            var objList = serviceProvider.GetPropertyValue(ResolvedServicesPropertyName)//返回Dictionary<Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceCacheKey,object>的装箱后的object
                .GetPropertyValue("Values")
                as IEnumerable<object>;

            return objList ?? new List<object>();
        }

        /// <summary>
        /// 获取容器引擎（ServiceProviderEngine）对象
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static object GetEngine(this IServiceProvider serviceProvider)
        {
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
    }
}