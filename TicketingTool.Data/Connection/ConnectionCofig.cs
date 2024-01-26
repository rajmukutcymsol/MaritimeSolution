using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Data.Connection
{
    public class ConnectionCofig
    {
        public static String ConnectionStr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            }
        }
    }
}
