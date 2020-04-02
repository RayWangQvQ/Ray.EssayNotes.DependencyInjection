using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ray.DependencyInjection.Attributes;
using Ray.DependencyInjection.Enums;

namespace Ray.DependencyInjection.Extensions
{
    /// <summary>
    /// 机器猫注册方法扩展
    /// </summary>
    public static class RobotCatExtensions
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="cat">容器</param>
        /// <param name="from">服务类型</param>
        /// <param name="to">服务实现类型</param>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        public static RobotCat Register(this RobotCat cat, Type from, Type to, LifetimeEnum lifetime)
        {
            Func<RobotCat, Type[], object> factory = (robotCat, arguments) => Create(robotCat, to, arguments);
            cat.Register(new ServiceRegistry(from, lifetime, factory));
            return cat;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TFrom">服务</typeparam>
        /// <typeparam name="TTo">服务实现</typeparam>
        /// <param name="cat">容器</param>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        public static RobotCat Register<TFrom, TTo>(this RobotCat cat, LifetimeEnum lifetime) where TTo : TFrom
            => cat.Register(typeof(TFrom), typeof(TTo), lifetime);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="cat">容器</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">实例对象</param>
        /// <returns></returns>
        public static RobotCat Register(this RobotCat cat, Type serviceType, object instance)
        {
            Func<RobotCat, Type[], object> factory = (robotCat, arguments) => instance;
            cat.Register(new ServiceRegistry(serviceType, LifetimeEnum.Root, factory));
            return cat;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务泛型</typeparam>
        /// <param name="cat">容器</param>
        /// <param name="instance">实例泛型</param>
        /// <returns></returns>
        public static RobotCat Register<TService>(this RobotCat cat, TService instance)
        {
            Func<RobotCat, Type[], object> factory = (robotCat, arguments) => instance;
            cat.Register(new ServiceRegistry(typeof(TService), LifetimeEnum.Root, factory));
            return cat;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="cat">容器</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">实例化委托</param>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        public static RobotCat Register(this RobotCat cat, Type serviceType,
            Func<RobotCat, object> factory, LifetimeEnum lifetime)
        {
            cat.Register(new ServiceRegistry(serviceType, lifetime, (_, arguments) => factory(_)));
            return cat;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务泛型</typeparam>
        /// <param name="cat">容器</param>
        /// <param name="factory">实例化委托</param>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        public static RobotCat Register<TService>(this RobotCat cat,
            Func<RobotCat, TService> factory, LifetimeEnum lifetime)
        {
            cat.Register(new ServiceRegistry(typeof(TService), lifetime, (_, arguments) => factory(_)));
            return cat;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="cat">容器</param>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        public static RobotCat Register(this RobotCat cat, Assembly assembly)
        {
            var typedAttributes = from type in assembly.GetExportedTypes()
                                  let attribute = type.GetCustomAttribute<MapToAttribute>()
                                  where attribute != null
                                  select new { ServiceType = type, Attribute = attribute };
            foreach (var typedAttribute in typedAttributes)
            {
                cat.Register(typedAttribute.Attribute.ServiceType, typedAttribute.ServiceType, typedAttribute.Attribute.Lifetime);
            }
            return cat;
        }

        /// <summary>
        /// 创建子容器
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public static RobotCat CreateChild(this RobotCat cat) => new RobotCat(cat);

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cat"></param>
        /// <returns></returns>
        public static T GetService<T>(this RobotCat cat) => (T)cat.GetService(typeof(T));

        /// <summary>
        /// 获取服务实例集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cat"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetServices<T>(this RobotCat cat) => cat.GetService<IEnumerable<T>>();

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="cat">容器</param>
        /// <param name="type">服务实现类型</param>
        /// <param name="genericArguments">服务实现的泛型参数类型</param>
        /// <returns></returns>
        private static object Create(RobotCat cat, Type type, Type[] genericArguments)
        {
            //1.泛型参数
            if (genericArguments.Length > 0)
            {
                type = type.MakeGenericType(genericArguments);//指定服务实现的泛型类型
            }
            //2.构造函数
            ConstructorInfo[] constructors = type.GetConstructors();
            if (constructors.Length == 0)//没有public的构造函数
            {
                throw new InvalidOperationException($"Cannot create the instance of {type} which does not have an public constructor.");
            }
            ConstructorInfo constructorInfo = constructors.FirstOrDefault(it => it.GetCustomAttributes(false).OfType<InjectionAttribute>().Any());
            constructorInfo ??= constructors.First();
            //3.构造参数
            ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
            //4.构造
            if (parameterInfos.Length == 0)
            {
                return Activator.CreateInstance(type);
            }
            var arguments = new object[parameterInfos.Length];
            for (int index = 0; index < arguments.Length; index++)
            {
                arguments[index] = cat.GetService(parameterInfos[index].ParameterType);
            }
            return constructorInfo.Invoke(arguments);
        }

    }
}
