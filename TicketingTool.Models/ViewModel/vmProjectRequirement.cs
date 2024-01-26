using System.Web;
using TicketingTool.Models.Models;

namespace TicketingTool.Models.ViewModel
{
    public class vmProjectRequirement:project_requirement
    {
        public HttpPostedFileBase mop_sop_attachment_path { get; set; }
        public string created_by { get; set; }
    }
}
