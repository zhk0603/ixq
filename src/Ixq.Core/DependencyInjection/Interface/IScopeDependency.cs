namespace Ixq.Core.DependencyInjection
{
    /// <summary>
    ///     实现此接口的类型将自动注册为<see cref="ServiceLifetime.Scoped" />模式
    /// </summary>
    public interface IScopeDependency : IDependency
    {
    }
}