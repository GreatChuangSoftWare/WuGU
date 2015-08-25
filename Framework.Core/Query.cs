using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class Query
    {
        public Query()
        {
            int QuryType = 0;
        }
        /// <summary>
        /// 查询条件类型:0：一般查询，1：自定义条件查询
        /// </summary>
        public int QuryType { get; set; }
        public string FieldName { get; set; }
        public string Oper { get; set; }
        public string Value { get; set; }
        public string AndOr { get; set; }
    }

    public class Querys : List<Query>
    {
        Dictionary<string, Query> _QueryDics = new Dictionary<string, Query>();
        public Dictionary<string, Query> QueryDicts
        {
            get {
                if (_QueryDics.Count == 0)
                {
                    //var temps = this.Where(p => p.FieldName != null);
                    var mm = (from p in this
                             group p by p.FieldName into g
                             where g.Count()>1
                             select g.Key);
                   this.RemoveAll(p =>mm.Contains( p.FieldName));

                    _QueryDics=this.ToDictionary(p => p.FieldName);
                }
                return _QueryDics;
            }
        }

        public void Clear()
        {
            _QueryDics = new Dictionary<string, Query>();
        }

        public string GetValue(string FieldName)
        {
            //Dictionary<string, Query> _QueryDicts = this.ToDictionary(p => p.FieldName);

            if (QueryDicts.ContainsKey(FieldName))
                return QueryDicts[FieldName].Value;
            else
                return "";
        }
    }
}
