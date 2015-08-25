using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//namespace Framework.Web.Mvc
namespace SoftPlatform.Controllers
{
    /// <summary>
    /// 控制器：Pre_RolesPremSet(用户角色)
    /// </summary>
    public class Pre_RolePremSetController : BaseController
    {
        public Pre_RolePremSetController()
        {
        }

        /// <summary>
        /// 编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditList(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Pre_RolePremSet_EditList();
            return View("EditList", resp);
        }
    }
}
