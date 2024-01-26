using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_user_detail
    {
        public List<vm_menu> menuDetail { get; set; }
        public vm_user_registration userDetail { get; set; }
    }
}
