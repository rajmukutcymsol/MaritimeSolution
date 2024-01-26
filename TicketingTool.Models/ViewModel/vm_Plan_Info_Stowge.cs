using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_Plan_Info_Stowge
    {
        public Guid id { get; set; }
        public string auto_id { get; set; }
        public Guid id_ref { get; set; }
        public string distence { get; set; }
        public string hold1 { get; set; }
        public string hold2 { get; set; }
        public string hold3 { get; set; }
        public string hold4 { get; set; }
    }
}
