using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_project_region_mapping
    {
        public Guid project { get; set; }
        public Guid region { get; set; }
        public string project_name { get; set; }
        public string region_name { get; set; }
        public Guid id { get; set; }
    }
}
