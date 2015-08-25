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
    public class Sys_OperLogDetailController : BaseController
    {
        /// <summary>
        /// 操作日志--根据类别主键查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ByOperLogIdent")]
        public ActionResult ByOperLogIdent(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Sys_OperLogDetail_ByOperLogIdent();
            return View("ByOperLogIdentN", resp); ;
        }

        /// <summary>
        /// 操作日志--根据类别主键查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ByFraLogIdent(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query
            {
                QuryType = 0,
                FieldName = "OperLogIdent___equal",
                //Value = LoginInfo.CurrNavIdent.ToString()
            });
            domain.PageQueryBase.RankInfo = "CreateDate|0";
            var resp = domain.Sys_OperLogDetail_Index();

            return View("Index", resp); ;
        }

    }
}

