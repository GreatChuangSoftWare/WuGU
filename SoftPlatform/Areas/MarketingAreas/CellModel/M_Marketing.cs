
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    /// <summary>
    /// 表：M_Marketing(营销课堂)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 营销课程ID
        /// </summary>
        public int? M_MarketingID { get; set; }

        /// <summary>
        /// 营销类别ID
        /// </summary>
        //public  int?  M_MarketingCategoryID{get;set;}

        /// <summary>
        /// 课程名称
        /// </summary>
        public  string  MarketingName{get;set;}

        /// <summary>
        /// 教室名称
        /// </summary>
        public  string  RoomName{get;set;}

        /// <summary>
        /// 教室时间
        /// </summary>
        public  DateTime?  RoomDate{get;set;}

        /// <summary>
        /// 时长
        /// </summary>
        public  int?  TimeLen{get;set;}

        /// <summary>
        /// 主讲老师
        /// </summary>
        public  string  Teacher{get;set;}

        /// <summary>
        /// 主讲人邀请码
        /// </summary>
        public  string  TeacherActivationCode{get;set;}

        /// <summary>
        /// 讲义
        /// </summary>
        public  string  HandoutFileName{get;set;}

        /// <summary>
        /// 讲义
        /// </summary>
        public  string  HandoutDown{get;set;}

        /// <summary>
        /// 讲义标识
        /// </summary>
        public  string  HandoutFileNameGuid{get;set;}

        /// <summary>
        /// 视频
        /// </summary>
        public  string  VideoName{get;set;}

        /// <summary>
        /// 视频
        /// </summary>
        public  string  VideoDown{get;set;}

        /// <summary>
        /// 视频标识
        /// </summary>
        public  string  VideoFileNameGuid{get;set;}

        /// <summary>
        /// 文件类型
        /// </summary>
        public  string  VideoFileType{get;set;}

        /// <summary>
        /// 文件大小
        /// </summary>
        public  int?  VideoFileSize{get;set;}

        /// <summary>
        /// 文件大小
        /// </summary>
        public  string  VideoFileSizeDisp{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  MarketingStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  MarketingStatuName{get;set;}

        /// <summary>
        /// 课程描述
        /// </summary>
        public  string  MarketingContext{get;set;}

        public SoftProjectAreaEntity M_Marketing { get; set; }
        public List<SoftProjectAreaEntity> M_Marketings { get; set; }
    }
}
