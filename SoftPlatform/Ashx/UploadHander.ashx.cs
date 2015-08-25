using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace SoftPlatform.Ashx
{
    /// <summary>
    /// UploadHander 的摘要说明
    /// </summary>
    public class UploadHander : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["Filedata"];
            var folder = context.Request["folder"];
            var IdentValue = context.Request["IdentValue"];
            string uploadPath = HttpContext.Current.Server.MapPath("/") + folder;
            if (file != null)
            {
                //获取文件后缀名
                string extension = file.FileName;
                string ext = extension.Substring(extension.LastIndexOf('.'));
                //生成新文件名
                string fileName = extension.ToLower();
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);// + "\\original");
                }
                //保存原图
                fileName = IdentValue+'_'+Guid.NewGuid().ToString();
                fileName = string.Format("/{0}{1}", fileName, ext);
                var phfilepath = uploadPath + fileName;
                file.SaveAs(phfilepath);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write(string.Format("{0}", folder+fileName));
            }
            else
            {
                context.Response.Write("0");
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}