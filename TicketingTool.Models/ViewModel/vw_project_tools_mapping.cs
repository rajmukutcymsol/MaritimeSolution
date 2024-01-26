using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Masters;

namespace TicketingTool.Models.ViewModel
{
    public class vw_project_tools_mapping
    {
        public Guid projectid { get; set; }
        public Guid toolid { get; set; }
        public string project_name { get; set; }
        public Guid id { get; set; }
        public string tool_name { get; set; }

    }
}
