using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_search
    {
        public string request_title { get; set; }
        public string auto_id { get; set; }
        public Guid? request_priority { get; set; }
        public Guid? request_status { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string user_name { get; set; }
        public string access_role { get; set; }
    }
}
