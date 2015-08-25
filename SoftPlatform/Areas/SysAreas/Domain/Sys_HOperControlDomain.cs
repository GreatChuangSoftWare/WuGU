using Framework.Core;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Sys
{
    public class Sys_HOperControlDomain //: DomainBase<Sys_HOperControl>
    {
        public List<SoftProjectAreaEntity> GetAll()
        {
            MyADORepository dal = new MyADORepository();
            string TSql = "SELECT * FROM  Design_ModularOrFunSql Order By OperCode";
            SqlParameter[] paras = new SqlParameter[] { };
            var Sys_HOperControls =dal.SelectCells(CommandType.Text, TSql, paras);
            return Sys_HOperControls;
        }
    }
}
