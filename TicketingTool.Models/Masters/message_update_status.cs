using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class message_update_status
    {
        public int id { get; set; }
        public string message { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string auto_id { get; set; }
        public int STATUS { get; set; }
        public Guid request_status { get; set; }
        public string request_title { get; set; }
        public Guid RequirementId { get; set; }
    }
}
