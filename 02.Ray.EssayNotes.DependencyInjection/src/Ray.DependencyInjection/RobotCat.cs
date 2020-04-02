using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Ray.DependencyInjection.Enums;

namespace Ray.DependencyInjection
{
    /// <summary>
    /// 机器猫哆啦A梦（容器）
    /// </summary>
    public class RobotCat : IServiceProvider, IDisposable
    {
        /// <summary>
        /// 父容器
        /// （根容器）
        /// </summary>
        internal readonly RobotCat Root;

        /// <summary>
        /// 愿望清单Dic
        /// </summary>
        internal readonly ConcurrentDictionary<Type, ServiceRegistry> RegistryDic;
        /// <summary>
        /// 非Transient的服务实例Dic
        /// </summary>
        private readonly ConcurrentDictionary<Key, object> _serviceDic;

        /// <summary>
        /// 实现了IDisposable接口（可释放的）服务实例集合
        /// </summary>
        private readonly ConcurrentBag<IDisposable> _disposables;
        /// <summary>
        /// 是否已释放服务实例
        /// </summary>
        private volatile bool _disposed;

        /// <summary>
        /// 构造一个根容器
        /// </summary>
        public RobotCat()
        {
            Root = this;//如果没有父容器，说明自己就是根容器，指向自己

            RegistryDic = new ConcurrentDictionary<Type, ServiceRegistry>();
            _serviceDic = new ConcurrentDictionary<Key, object>();

            _disposables = new ConcurrentBag<IDisposable>();
        }

        /// <summary>
        /// 构造一个非根容器
        /// </summary>
        /// <param name="parent">父容器</param>
        internal RobotCat(RobotCat parent)
        {
            Root = parent.Root;//指向父容器的_root，如果父容器也是非根容器，则继续指向父容器，最后其实指向的就是根容器

            RegistryDic = Root.RegistryDic;//注册表dic全部都存储在根容器中
            _serviceDic = new ConcurrentDictionary<Key, object>();//实例对象存储在当前容器中

            _disposables = new ConcurrentBag<IDisposable>();
        }

        /// <summary>
        /// 确保实例对象没有被释放
        /// </summary>
        private void EnsureNotDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("RobotCat");
            }
        }

        /// <summary>
        /// 注册（即向机器猫添加愿望清单）
        /// </summary>
        /// <param name="registry"></param>
        /// <returns></returns>
        public RobotCat Register(ServiceRegistry registry)
        {
            EnsureNotDisposed();

            if (RegistryDic.TryGetValue(registry.ServiceType, out var existing))//已被注册过
            {
                registry.Next = existing;
                RegistryDic[registry.ServiceType] = registry;
            }
            else//未被注册过
            {
                RegistryDic[registry.ServiceType] = registry;
            }
            return this;
        }

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <param name="serviceType">服务实现类型</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            EnsureNotDisposed();

            if (serviceType == typeof(RobotCat) || serviceType == typeof(IServiceProvider))
            {
                return this;
            }

            ServiceRegistry registry;

            //IEnumerable<T>
            if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var elementType = serviceType.GetGenericArguments()[0];
                if (!RegistryDic.TryGetValue(elementType, out registry))
                {
                    return Array.CreateInstance(elementType, 0);
                }

                var registries = registry.AsEnumerable();
                var services = registries.Select(it => GetServiceCore(it, Type.EmptyTypes)).ToArray();
                Array array = Array.CreateInstance(elementType, services.Length);
                services.CopyTo(array, 0);
                return array;
            }

            //Generic
            if (serviceType.IsGenericType && !RegistryDic.ContainsKey(serviceType))
            {
                var definition = serviceType.GetGenericTypeDefinition();
                return RegistryDic.TryGetValue(definition, out registry)
                    ? GetServiceCore(registry, serviceType.GetGenericArguments())
                    : null;
            }

            //Normal
            return RegistryDic.TryGetValue(serviceType, out registry)
                    ? GetServiceCore(registry, new Type[0])
                    : null;
        }

        public void Dispose()
        {
            _disposed = true;
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
            _serviceDic.Clear();
        }

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <param name="registry">注册表</param>
        /// <param name="genericArguments">实例化所需的泛型参数</param>
        /// <returns></returns>
        private object GetServiceCore(ServiceRegistry registry, Type[] genericArguments)
        {
            var key = new Key(registry, genericArguments);

            switch (registry.Lifetime)
            {
                case LifetimeEnum.Root:
                    return GetOrCreate(Root._serviceDic, Root._disposables);
                case LifetimeEnum.Self:
                    return GetOrCreate(_serviceDic, _disposables);
                case LifetimeEnum.Transient:
                    var service = registry.Factory(this, genericArguments);
                    if (service is IDisposable disposable && disposable != this)
                    {
                        _disposables.Add(disposable);
                    }
                    return service;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            object GetOrCreate(ConcurrentDictionary<Key, object> serviceDic, ConcurrentBag<IDisposable> disposables)
            {
                if (serviceDic.TryGetValue(key, out var service))//存在实例
                {
                    return service;
                }

                //不存在
                service = registry.Factory(this, genericArguments);//创建
                serviceDic[key] = service;//添加到服务实例字典
                if (service is IDisposable disposable)//添加到可释放实例字典
                {
                    disposables.Add(disposable);
                }
                return service;
            }
        }
    }
}