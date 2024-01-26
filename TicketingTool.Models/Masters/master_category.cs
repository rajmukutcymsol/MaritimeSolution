using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_category
    {
        public Guid id { get; set; }
        public string category_name { get; set; }
        public string category_name_discription { get; set; }
        public bool is_active { get; set; }
    }
}
