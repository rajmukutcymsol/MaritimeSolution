using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_cli_ui
    {
        public Guid id { get; set; }
        public string cli_ui_name { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string fulladdress { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public System.Web.HttpPostedFileBase upload { get; set; }
        public string logoupload { get; set; }

        public string phone { get; set; }
    }

}
