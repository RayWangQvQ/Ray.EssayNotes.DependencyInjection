using System;
using Ray.DependencyInjection.Enums;

namespace Ray.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class MapToAttribute : Attribute
    {
        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get; }
        /// <summary>
        /// 生命周期
        /// </summary>
        public LifetimeEnum Lifetime { get; }

        public MapToAttribute(Type serviceType, LifetimeEnum lifetime)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }
    }
}