using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_update_history
    {
        public int id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string DateofUpdate { get; set; }
        public string auto_id { get; set; }
        public Guid RequirementId { get; set; }
        public string Updated_By { get; set; }
        public string employee_name { get; set; }
        public string employee_id { get; set; }
        public string times { get; set; }
        public string descc { get; set; }
        public string update_date { get; set; }

    }
}
