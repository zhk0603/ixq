using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.UI.Controls
{
    /// <summary>
    ///     页面视图模型接口。
    /// </summary>
    public interface IPageViewModel<TEntity,TKey>
        where TEntity : class, IEntity<TKey>
    {
        IRuntimeEntityMenberInfo RuntimeEntityMenberInfo { get; set; }
        Type EntityType { get; set; }
        Type DtoType { get; set; }
        IPageConfig PageConfig { get; set; }
        Pagination Pagination { get; set; }
        string GetColNames();
        string GetColModel();
    }

    public interface IPageViewModel : IPageViewModel<IEntity<Guid>, Guid> 
    {
    }
}
