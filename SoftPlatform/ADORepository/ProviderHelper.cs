using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Framework.Web.Mvc.ADORepository
{
    public class ProviderHelper
    {
        private static string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        static ProviderHelper()
        {
        }

        public static string ConnectionString
        {
            get { return ConfigurationManager.AppSettings["ConnectionString"]; }
        }
    }
}