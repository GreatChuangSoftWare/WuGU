using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftPlatform.Controllers;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftProject.Domain
{
    public partial class SoftProjectAreaEntityDomain
    {
        //MyResponseBase<LoginInfo> resp = new MyResponseBase<LoginInfo>();

        public MyResponseBase Login()
        {
            if (string.IsNullOrEmpty(Item.MobilePhone) || string.IsNullOrEmpty(Item.PasswordDigest))
            {
                throw new Exception("手机号和密码不能为空");
                //resp.RespAttachInfo.ValidationErrors.ErrorMessage = "用户名或密码不能为空";
                //return resp;
            }
            //return false;

            #region 用户登录

            var Users = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.MobilePhone == Item.MobilePhone && p.PasswordDigest == Item.PasswordDigest && p.UserStatuID == 1);

            if (Users.Count() > 0)
            {
                #region 公司
                var user = Users.First();
                //HomePageName HomePageUrl
                var loginInfo = new SoftProjectAreaEntity
                {
                    Sys_LoginInfoID = user.Pre_UserID,
                    LoginCategoryID = 1,
                    MobilePhone = user.MobilePhone,
                    CompanyID = user.Pre_CompanyID,
                    CompanyName = user.PreCompanyName,
                    UserName = user.UserName,
                    RoleID = user.Pre_RoleID,

                    HomePageName = user.RoleHomePageName,
                    HomePageUrl = user.RoleHomePageUrl,
                };
                resp.Item = loginInfo;
                #endregion
            }
            else
            {
                throw new Exception("用户、密码不正确，或此用户已被停用!");
            }
            #endregion
            return resp;
        }

        //public MyResponseBase ChangePassSave()
        //{
        //    var resp = new MyResponseBase();
        //    if (Item.PasswordDigest != null && Item.RePasswordDigest != null)
        //    {
        //        if (Item.PasswordDigest != Item.RePasswordDigest)
        //        {
        //            throw new Exception("密码与确认密码不一致");
        //            //resp.RespAttachInfo.ValidationErrors.Add(new ValidationInfo { FieldName = "RePasswordDigest", Title = "确认密码", Message = "密码与确认密码不一致" });//.ErrorMessage = "密码与确认密码不一致";
        //        }
        //        else
        //        {
        //            if (Item.LoginCategoryID == 1)//用户登录：修改密码
        //            {
        //                Pre_User_ChangPass();
        //            }
        //            else
        //            {
        //                Comp_User_ChangPass();
        //            }
        //        }
        //    }
        //    return resp;
        //}
    }
}
