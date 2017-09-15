using Ixq.Core.Entity;
using Ixq.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     实体模型映射对象。
    /// </summary>
    public class EntityModelBinder : DefaultModelBinder
    {
        /// <summary>
        ///     在更新模型后调用。
        /// </summary>
        /// <param name="controllerContext">运行控制器的上下文。上下文信息包括控制器、HTTP 内容、请求上下文和路由数据。</param>
        /// <param name="bindingContext">绑定模型的上下文。上下文包含模型对象、模型名称、模型类型、属性筛选器和值提供程序等信息。</param>
        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.OnModelUpdated(controllerContext, bindingContext);
            ModelStateUpdate(controllerContext, bindingContext);
        }

        /// <summary>
        ///     更新模型状态。
        /// </summary>
        /// <param name="controllerContext">运行控制器的上下文。上下文信息包括控制器、HTTP 内容、请求上下文和路由数据。</param>
        /// <param name="bindingContext">绑定模型的上下文。上下文包含模型对象、模型名称、模型类型、属性筛选器和值提供程序等信息。</param>
        protected virtual void ModelStateUpdate(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            foreach (var modelStateDic in bindingContext.ModelState)
            {
                OnModelStateUpdate(controllerContext, bindingContext, modelStateDic);
            }
        }
        protected virtual void OnModelStateUpdate(ControllerContext controllerContext, ModelBindingContext bindingContext, KeyValuePair<string, ModelState> modelStateDic)
        {
            if (modelStateDic.Value.Errors.Count > 0)
            {
                var entityControllerDescriptor = controllerContext.Controller as IEntityControllerDescriptor;
                if (entityControllerDescriptor != null)
                {
                    UpdateModelStateWithEntityMetadata(entityControllerDescriptor.EntityMetadata, bindingContext, modelStateDic);
                }
            }
        }

        /// <summary>
        ///     使用实体元数据更新模型状态。
        /// </summary>
        /// <param name="metadata">实体元数据。</param>
        /// <param name="bindingContext">绑定模型的上下文。上下文包含模型对象、模型名称、模型类型、属性筛选器和值提供程序等信息。</param>
        /// <param name="modelStateDic">模型状态键值对。在模型绑定时发生错误的模型状态。</param>
        protected virtual void UpdateModelStateWithEntityMetadata(IEntityMetadata entityMetadata, ModelBindingContext bindingContext, KeyValuePair<string, ModelState> modelStateDic)
        {
            // 当前授权用户没有此属性的编辑权限时，清除模型错误信息。
            if (!HasEditProperty(entityMetadata.EditPropertyMetadatas, modelStateDic.Key))
            {
                modelStateDic.Value.Errors.Clear();
            }
        }

        private bool HasEditProperty(IEntityPropertyMetadata[] metadata, string name)
        {
            // TODO 关联属性错误处理。
            return metadata.Any(x => x.PropertyName == name);
        }
    }
}
