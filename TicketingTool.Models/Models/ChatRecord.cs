using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Models
{
    public class ChatRecord
    {
        public int ChatId { get; set; }
        public string ChatMessage { get; set; }
        public string ChatTime { get; set; }
        public string auto_id { get; set; }
        public string ChatCreatedBy { get; set; }
        public string employee_name { get; set; }
    }
}
