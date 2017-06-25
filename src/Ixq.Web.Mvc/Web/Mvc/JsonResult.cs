using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Ixq.Web.Mvc
{
    public class JsonResult : System.Web.Mvc.JsonResult
    {
        public JsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }
        public JsonSerializerSettings SerializerSettings { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "此请求已被阻止，因为敏感信息可以在GET请求中使用时向第三方网站披露。 要允许GET请求，请将JsonRequestBehavior设置为AllowGet。");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
                JsonSerializer serializer = SerializerSettings == null
                    ? JsonSerializer.Create()
                    : JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }

}
