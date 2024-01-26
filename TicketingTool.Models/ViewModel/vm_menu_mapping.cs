using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Masters;

namespace TicketingTool.Models.ViewModel
{
    public class vm_menu_mapping
    {
        public List<master_menu> master_Menu { get; set; }
        public List<master_role> roles { get; set; }
        master_role role { get; set; }
    }
}
