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
        IDatagridConfig DatagridConfig { get; set; }
        IDto<TEntity, TKey>[] Items { get; set; }
    }
    public interface IDatagrid : IDatagrid<IEntity<Guid>, Guid>
    {
    }
}
