namespace Ixq.Core.Entity
{
    /// <summary>
    ///     实体接口
    /// </summary>
    public interface IEntity<TKey>
    {
        /// <summary>
        ///     获取或设置主键。
        /// </summary>
        TKey Id { get; set; }
    }
}