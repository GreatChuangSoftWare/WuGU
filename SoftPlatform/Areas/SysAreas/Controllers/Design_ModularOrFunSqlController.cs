using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class Design_ModularOrFunSqlController : BaseController
    {
        public Design_ModularOrFunSqlController()
        {
        }

        #region 编辑

        [HttpGet]
        public ActionResult EditList(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunSql_GetByModularOrFunID();
            //resp.FunNameEn = "Edit";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("EditList", resp);
        }

        [HttpPost]
        public HJsonResult EditListSave(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            //DBTSql
            //    DBSqlParam
            //domain.Item.DBTSql = Server.UrlDecode(domain.Item.DBTSql);
            //domain.Item.DBSqlParam = Server.UrlDecode(domain.Item.DBSqlParam);
            domain.Item.Items.ForEach(p =>
                {
                    //p.DBTSql = Server.UrlDecode(p.DBTSql);
                    if (p.DBTSql != null)
                    {
                        p.DBTSql = p.DBTSql.Replace("+", "##########");
                        p.DBTSql = Server.UrlDecode(p.DBTSql);
                        p.DBTSql = p.DBTSql.Replace("##########", "+");
                    }
                    p.DBSqlParam = Server.UrlDecode(p.DBSqlParam);
                });

            ;
            var resp = domain.Design_ModularOrFunSql_EditListSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            return View("Row", resp);
        }

        /// <summary>
        /// 生成Sql语句
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult BulidTSql(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunSql_BulidTSql();
            return new HJsonResult(new { Data = resp });
        }

    }
}

