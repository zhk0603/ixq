using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     返回一个Json格式的模型错误操作结果。
    /// </summary>
    public class ModelErrorResult : JsonResult
    {
        private ModelStateDictionary _dic;
        public ModelErrorResult(ModelStateDictionary modelState)
        {
            _dic = modelState ?? throw new ArgumentNullException(nameof(modelState));
        }

        public override void ExecuteResult(ControllerContext context)
        {
            ExecuteResultCore(_dic);
            base.ExecuteResult(context);
        }
        public virtual void ExecuteResultCore(ModelStateDictionary modelState)
        {
            base.Data = modelState.Where(x => x.Value.Errors.Count > 0).ToList();
        }
    }
}
