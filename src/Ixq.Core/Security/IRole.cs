using Ixq.Core.Entity;

namespace Ixq.Core.Security
{
    public interface IRole<TKey> : IEntity<TKey>
    {
        string Name { get; set; }
    }
}