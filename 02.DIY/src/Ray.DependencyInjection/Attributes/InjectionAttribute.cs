using System;

namespace Ray.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionAttribute : Attribute { }
}