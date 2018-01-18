using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     请求结果。
    /// </summary>
    public class RequestResult
    {
        /// <summary>
        ///     表示当前请求是否成功。
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     获取或设置错误码。
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        ///     获取或设置错误信息。
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
