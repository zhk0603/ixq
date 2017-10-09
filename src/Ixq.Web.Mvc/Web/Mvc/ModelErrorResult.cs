using System;
using System.Linq;
using System.Web.Mvc;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     返回一个Json格式的模型错误操作结果。
    /// </summary>
    public class ModelErrorResult : JsonResult
    {
        private readonly ModelStateDictionary _dic;

        /// <summary>
        ///     初始化一个 <see cref="T:Ixq.Web.Mvc.ModelErrorResult" /> 实例。
        /// </summary>
        /// <param name="modelState"></param>
        public ModelErrorResult(ModelStateDictionary modelState)
        {
            _dic = modelState ?? throw new ArgumentNullException(nameof(modelState));
        }

        /// <summary>
        ///     通过从 <see cref="ActionResult" /> 类继承的自定义类型，启用对操作方法结果的处理。
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            ExecuteResultCore(_dic);
            base.ExecuteResult(context);
        }

        /// <summary>
        ///     通过从 <see cref="ActionResult" /> 类继承的自定义类型，启用对操作方法结果的处理核心。
        /// </summary>
        /// <param name="modelState"></param>
        public virtual void ExecuteResultCore(ModelStateDictionary modelState)
        {
            Data = modelState.Where(x => x.Value.Errors.Count > 0).ToList();
        }
    }
}