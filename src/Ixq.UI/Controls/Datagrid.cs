using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.UI.ComponentModel.DataAnnotations;
using Ixq.UI.Controls;

namespace Ixq.UI.Controls
{
    public class Datagrid<TEntity, TDto> : IDatagrid where TDto : class, IDto<TEntity, Guid>, new()
    {
        public IDatagridConfig DatagridConfig { get; set; }
        public IDto<IEntity<Guid>, Guid>[] Items { get; set; }
    }
}
