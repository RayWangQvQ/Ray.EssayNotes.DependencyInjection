namespace Ray.DependencyInjection.Enums
{
    public enum LifetimeEnum
    {
        /// <summary>
        /// 全局单例
        /// </summary>
        Root,
        /// <summary>
        /// 域内单例
        /// </summary>
        Self,
        /// <summary>
        /// 瞬时单例
        /// </summary>
        Transient
    }
}