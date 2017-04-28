using Ixq.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.UI.ComponentModel.DataAnnotations;

namespace Ixq.UI.Controls
{
    public interface IDatagrid<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        DatagridAttribute DatagridAttribute { get; set; }
        Type EntityType { get; set; }
        Type DtoType { get; set; }
        Pagination Pagination { get; set; }
        IDto<TEntity, TKey>[] Items { get; set; }
        PropertyInfo[] ColumnsPropertyInfo { get; set; }
    }
    public interface IDatagrid : IDatagrid<IEntity<Guid>, Guid>
    {

    }
}
