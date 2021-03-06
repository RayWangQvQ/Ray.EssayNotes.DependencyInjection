﻿using System;

namespace Ray.DependencyInjection
{
    internal class Key : IEquatable<Key>
    {
        /// <summary>
        /// 愿望清单（注册信息表）
        /// </summary>
        public ServiceRegistry Registry { get; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public Type[] GenericArguments { get; }

        public Key(ServiceRegistry registry, Type[] genericArguments)
        {
            Registry = registry;
            GenericArguments = genericArguments;
        }

        public bool Equals(Key other)
        {
            if (Registry != other.Registry)
            {
                return false;
            }
            if (GenericArguments.Length != other.GenericArguments.Length)
            {
                return false;
            }
            for (int index = 0; index < GenericArguments.Length; index++)
            {
                if (GenericArguments[index] != other.GenericArguments[index])
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = Registry.GetHashCode();
            for (int index = 0; index < GenericArguments.Length; index++)
            {
                hashCode ^= GenericArguments[index].GetHashCode();
            }
            return hashCode;
        }
        public override bool Equals(object obj) => obj is Key key ? Equals(key) : false;
    }

}