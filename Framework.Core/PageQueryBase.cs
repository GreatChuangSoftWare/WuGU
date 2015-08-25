using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class PageQueryBase
    {
        public PageQueryBase()
        {
            this.PageSize = 50;
            this.IsPagination = 0;
            this.RankInfos = new List<RankInfo>();
        }

        /// <summary>
        /// 是否分页:0：不分页，1：分页
        /// </summary>
        public int IsPagination { get; set; }

        public int _TotalItems;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalItems
        {
            get
            {
                return _TotalItems;
            }
            set
            {
                //_PageIndex = (value - 1) / PageSize + 1;
                _TotalItems = value;
            }
        }

        int _PageIndex = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }
        }

        public int SkipNum
        {
            get
            {
                return (_PageIndex - 1) * PageSize;
            }
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get { return ((int)TotalItems - 1) / (int)PageSize + 1; }
        }

        public string _RankInfo;

        public string RankInfo
        {
            get;
            set;
        }

        List<RankInfo> _RankInfos = new List<RankInfo>();
        /// <summary>
        /// 排序列表
        /// </summary>
        public List<RankInfo> RankInfos
        {
            get
            {
                List<RankInfo> RankInfoss = new List<RankInfo>();
                if (!string.IsNullOrEmpty(RankInfo))
                {
                    var val = RankInfo.Split('|');
                    RankInfoss.Add(new RankInfo { Property = val[0], Ascending = val[1] == "1" ? true : false });
                }
                return RankInfoss;
            }
            set
            {
                _RankInfos = value;
            }
        }

        bool _ShowPageSize = true;
        
        /// <summary>
        /// 是否显示分页
        /// </summary>
        public bool ShowPageSize { get { return _ShowPageSize; } set { _ShowPageSize=value;} }

        bool _ShowGoto = true;

        /// <summary>
        /// 显示Goto
        /// </summary>
        public bool ShowGoto { get { return _ShowGoto; } set { _ShowGoto = value; } }
    }

}
