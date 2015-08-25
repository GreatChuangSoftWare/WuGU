using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Framework.Web.Mvc.Sys.Repository
{
    public class Sys_HOperControlRepository : MyADORepository
    {
        public List<SoftProjectAreaEntity> GetAll()
        {
            //string TSql = "SELECT * FROM  Sys_HOperControl Order By OperCode";

            //
            string TSql = "SELECT * FROM  Design_ModularOrFunSql Order By OperCode";
            SqlParameter[] paras=new SqlParameter[]{};
            var Sys_HOperControls = SelectCells(CommandType.Text, TSql, paras);
            //var xx = Sys_HOperControls.Where(p => p.OperCode == "C_OutboundOrder.Add");
            return Sys_HOperControls;
        }
    }
}
