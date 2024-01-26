using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vw_project_customers_mapping
    {
        public Guid projectid { get; set; }
        public Guid customerid { get; set; }
        public string project_name { get; set; }
        public Guid id { get; set; }
        public string customer_name { get; set; }
    }
}
