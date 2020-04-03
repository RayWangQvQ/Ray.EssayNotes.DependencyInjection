using System;
using System.Collections.Generic;
using Ray.DependencyInjection.Enums;

namespace Ray.DependencyInjection
{
    /// <summary>
    /// 愿望清单
    /// 记录了想要什么服务、构造服务的方法、服务的作用域和有效时长等
    /// 机器猫会根据愿望清单从口袋里生成对应实物
    /// </summary>
    public class ServiceRegistry
    {
        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get; }
        /// <summary>
        /// 生命周期
        /// </summary>
        public LifetimeEnum Lifetime { get; }

        /// <summary>
        /// 用来创建服务实例的工厂
        /// Func委托中前两个给容器和创建服务所需要的泛型参数，出参为object
        /// </summary>
        public Func<RobotCat, Type[], object> Factory { get; }

        /// <summary>
        /// 链表，用于存储同一个服务类型（ServiceType属性相同）的多个ServiceRegistry
        /// </summary>
        internal ServiceRegistry Next { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="lifetime">生命周期</param>
        /// <param name="factory">实例化委托</param>
        public ServiceRegistry(Type serviceType, LifetimeEnum lifetime, Func<RobotCat, Type[], object> factory)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
            Factory = factory;
        }

        internal IEnumerable<ServiceRegistry> AsEnumerable()
        {
            var list = new List<ServiceRegistry>();
            for (var self = this; self != null; self = self.Next)
            {
                list.Add(self);
            }
            return list;
        }
    }
}
