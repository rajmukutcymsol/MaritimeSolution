using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_RoleMenu
    {
        public Guid? roleid { get; set; }
        public Guid? menuid { get; set; }
        public bool? is_active { get; set; }
    }
}
