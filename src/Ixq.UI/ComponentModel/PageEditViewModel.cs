using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel
{
    /// <summary>
    ///     页面编辑模型。
    /// </summary>
    public class PageEditViewModel<TDto, TKey> : IPageEditViewModel
        where TDto : class, IDto<IEntity<TKey>,TKey>, new()
    {
        /// <summary>
        ///     初始化一个<see cref="PageEditViewModel{TDto, TKey}"/>对象。
        /// </summary>
        public PageEditViewModel()
        {
        }
        /// <summary>
        ///     初始化一个<see cref="PageEditViewModel{TDto, TKey}"/>对象。
        /// </summary>
        /// <param name="entityDto">实体数据传输对象。</param>
        /// <param name="propertyMemberInfo">实体属性元数据。</param>
        public PageEditViewModel(TDto entityDto, IEntityPropertyMetadata[] propertyMemberInfo)
        {
            this._entityDto = entityDto;
            this.PropertyMenberInfo = propertyMemberInfo;
        }

        private TDto _entityDto;
        /// <summary>
        ///     获取或设置标题。
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///     获取或设置实体属性元数据。
        /// </summary>
        public IEntityPropertyMetadata[] PropertyMenberInfo { get; set; }
        /// <summary>
        ///     获取或设置实体数据传输对象。
        /// </summary>
        public object EntityDto
        {
            get { return _entityDto; }
            set
            {
                var entity = value as TDto;
                if (entity == null)
                    throw new InvalidCastException(
                        $"无法将类型为“{value.GetType().FullName}”的对象强制转换为类型“{typeof (TDto).FullName}”。");
                _entityDto = entity;
            }
        }
    }
}
