using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Framework.Web.Mvc
{
    public class HJsonResult : ActionResult
    {
        public HJsonResult()
        {

        }

        public HJsonResult(object data)
        {
            this.Data = data;
        }

        public Encoding ContentEncoding
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public object Data
        {
            get;
            set;
        }

        public JsonRequestBehavior JsonRequestBehavior
        {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                response.Write(JsonConvert.SerializeObject(Data));
            }
        }
    }

}