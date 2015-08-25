using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Framework.Web.Mvc
{
    public class Breadcrumb
    {
        public BreadcrumbItem Root { get; set; }
        public List<BreadcrumbItem> Items { get; set; }
        public string CurrentName { get; set; }

        private string _separator;

        public string Separator
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_separator))
                {
                    return "&nbsp;>&nbsp;";
                }
                else
                    return _separator;
            }
            set { _separator = value; }
        }
    }

    public class BreadcrumbItem
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string ActionCss { get; set; }
    }

    public static class HtmlHelpers2
    {
        //public static IHtmlString RenderBreadcrumb(this HtmlHelper helper)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    BaseController controller = helper.ViewContext.Controller as BaseController;

        //    if (controller != null && controller.Breadcrumb != null)
        //    {
        //        var breadcrumb = controller.Breadcrumb;
        //        if (breadcrumb != null)
        //        {
        //            //sb.Append("<ul class=\"page-breadcrumb breadcrumb\">");
        //            //breadcrumb myjuxing
        //            sb.Append("<ul class=\"breadcrumb myjuxing\">");
        //            if (breadcrumb.Root != null)
        //            {
        //                //sb.Append("<i class=\"icon-home\"></i>" + breadcrumb.Root.Name + "" + breadcrumb.Separator);
        //                if (string.IsNullOrWhiteSpace(breadcrumb.Root.URL))
        //                {
        //                    sb.Append("" + breadcrumb.Root.Name + "");// + breadcrumb.Separator);
        //                }
        //                else
        //                {
        //                    //<li><i class=\"fa fa-angle-right\"></i></li>
        //                    //<span class="icon-home"></span>
        //                    sb.Append("<li><span class='icon-home'></span><a href=\"" + breadcrumb.Root.URL + "\">" + breadcrumb.Root.Name + "</a></li>");
        //                }
        //            }

        //            if (breadcrumb.Items != null)
        //            {
        //                breadcrumb.Items.ForEach(m =>
        //                {
        //                    if (string.IsNullOrWhiteSpace(m.URL))
        //                    {
        //                        sb.Append("<li>" + m.Name + "</li>");//+ breadcrumb.Separator + "
        //                    }
        //                    else
        //                    {
        //                        sb.Append("<li><a href='" + m.URL + "'>" + m.Name + "</a></li>");
        //                    }

        //                });
        //            }
        //            if (helper.ViewBag.Title == null)
        //            {
        //                sb.Append("<li>" + breadcrumb.CurrentName + "</li>");
        //            }
        //            else
        //            {
        //                sb.Append("<li>" + helper.ViewBag.Title + "</li>");
        //            }
        //            sb.Append("</ul>");
        //        }
        //    }

        //    return helper.Raw(sb.ToString()); ;
        //}
    }
}