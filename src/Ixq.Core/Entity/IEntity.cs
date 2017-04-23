namespace Ixq.Core.Entity
{
    /// <summary>
    ///     实体接口
    /// </summary>
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}