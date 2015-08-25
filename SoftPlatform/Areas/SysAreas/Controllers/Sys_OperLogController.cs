using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class Sys_OperLogController : BaseController
    {

        /// <summary>
        /// 操作日志--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Sys_OperLog_Index();
            return View("Index", resp); ;
        }
    }
}

