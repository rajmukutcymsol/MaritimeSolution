using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_project_manager
    {
        public Guid? id { get; set; }
        public string project_manager_name { get; set; }
        public bool is_active { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
    }
}
