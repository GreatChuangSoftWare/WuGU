using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SoftProject.Domain
{
    public class ProjectCommon
    {
        //#region #区域

        //static List<SoftProjectAreaEntity> _Ba_Areas = new List<SoftProjectAreaEntity>();

        //public static List<SoftProjectAreaEntity> Ba_Areas
        //{
        //    get
        //    {
        //        if (_Ba_Areas.Count == 0)
        //        {
        //            SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
        //            _Ba_Areas = domain.Ba_Area_GetAll();
        //        }
        //        return _Ba_Areas;
        //    }
        //}

        ///// <summary>
        ///// 获取所有1级区域
        ///// </summary>
        //public static List<SoftProjectAreaEntity> Ba_AreaID1s
        //{
        //    get
        //    {
        //        var lists = Ba_Areas.Where(p => p.AreaParentCode == "0");
        //        return lists.ToList();
        //    }
        //}

        ///// <summary>
        ///// 根据ID，获取对应ID父区域下的子区域
        ///// </summary>
        ///// <param name="Ba_AreaID"></param>
        ///// <returns></returns>
        //public static List<SoftProjectAreaEntity> GetBrotherBa_AreaIDss(int? Ba_AreaID)
        //{
        //    var item = Ba_Areas.Where(p => p.Ba_AreaID == Ba_AreaID).First();

        //    var lists = Ba_Areas.Where(p => p.AreaParentCode == item.AreaParentCode);
        //    return lists.ToList();
        //}

        ///// <summary>
        ///// 根据ID，查询所有子区域
        ///// </summary>
        ///// <param name="Ba_AreaID"></param>
        ///// <returns></returns>
        //public static List<SoftProjectAreaEntity> GetSubBa_AreaIDss(int? Ba_AreaID)
        //{
        //    var item = Ba_Areas.Where(p => p.Ba_AreaID == Ba_AreaID).First();

        //    var lists = _Ba_Areas.Where(p => p.AreaParentCode == item.AreaCode);
        //    return lists.ToList();
        //}

        ///// <summary>
        ///// 根据ID查询
        ///// </summary>
        ///// <param name="Ba_AreaID"></param>
        ///// <returns></returns>
        //public static SoftProjectAreaEntity GetByAreaID(int? Ba_AreaID)
        //{
        //    var item = Ba_Areas.Where(p => p.Ba_AreaID == Ba_AreaID).First();

        //    return item;
        //}


        //#endregion

        public static SoftProjectAreaEntity BulidRequest(SoftProjectAreaEntity oldItem, SoftProjectAreaEntity ChildAction)
        {
            //Model.ChildAction.ControllName, Model.ChildAction
            var request = new SoftProjectAreaEntity();
            var ActionFieldNamess = ChildAction.ActionFieldNames.Split(',');
            Type type = oldItem.GetType();
            foreach (var fieldname in ActionFieldNamess)
            {
                PropertyInfo property = type.GetProperty(fieldname);
                object value = property.GetValue(oldItem, null);
                property.SetValue(request, value);
            }
            return request;
        }

        public static string FileSizeDisp(decimal? filesize)
        {
            var filesizeDisp = "";
            if (filesize > 1024 * 1024 * 1024)
            {
                filesizeDisp = (Convert.ToInt32((filesize * 100 / (1024 * 1024 * 1024))) / 100).ToString() + "G";
            }
            if (filesize > 1024 * 1024)
            {
                filesizeDisp = (Convert.ToInt32((filesize * 100 / (1024 * 1024))) / 100).ToString() + "M";
            }
            else if (filesize > 1024)
            {
                filesizeDisp = (Convert.ToInt32((filesize * 100 / (1024))) / 100).ToString() + "K";
            }
            return filesizeDisp;
        }
    }

}