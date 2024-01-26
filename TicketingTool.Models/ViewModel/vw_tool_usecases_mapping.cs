using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vw_tool_usecases_mapping
    {
        public Guid id { get; set; }
        public string tool_name { get; set; }
        public string use_case_name { get; set; }
        public Guid toolid { get; set; }
        public Guid usecaseid { get; set; }
        public Guid projectid { get; set; }
        public string project_name { get; set; }
        public string vendor_name { get; set; }
        public Guid venderid { get; set; }
        public string technology_name { get; set; }
        public Guid technologyid { get; set; }
        public string node_type_name { get; set; }
        public Guid nodetypeid { get; set; }
        public Guid vendor { get; set; }
        public Guid technology { get; set; }
        public Guid node_type { get; set; }

    }
}
