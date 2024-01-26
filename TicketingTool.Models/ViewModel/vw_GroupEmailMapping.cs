using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vw_GroupEmailMapping
    {
        public Guid id { get; set; }
        public Guid group_id { get; set; }
        public string employee_name { get; set; }
        public string employee_id { get; set; }
        public string email_id { get; set; }
        public string group_name { get; set; }
    }
}
