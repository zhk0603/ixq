using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel
{
    public class PageEditViewModel<TDto, TKey> : IPageEditViewModel
        where TDto : class, IDto<IEntity<TKey>,TKey>, new()
    {
        public PageEditViewModel(TDto entityDto, IEntityPropertyMetadata[] propertyMemberInfo)
        {
            this._entityDto = entityDto;
            this.PropertyMenberInfo = propertyMemberInfo;
        }
        public PageEditViewModel()
        {
        }

        private TDto _entityDto;
        public string Title { get; set; }
        public IEntityPropertyMetadata[] PropertyMenberInfo { get; set; }
        public object EntityDto
        {
            get { return _entityDto; }
            set
            {
                var entity = value as TDto;
                if (entity != null)
                    _entityDto = entity;
                throw new InvalidCastException(
                    $"无法将类型为“{value.GetType().FullName}”的对象强制转换为类型“{typeof(TDto).FullName}”。");
            }
        }

    }
}
