namespace Ixq.Core.Entity
{
    /// <summary>
    ///     实体接口
    /// </summary>
    public interface IEntity<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }
}