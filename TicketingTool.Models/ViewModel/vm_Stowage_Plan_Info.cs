using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_Stowage_Plan_Info
    {
        public string auto_id { get; set; }
        public Guid id_ref { get; set; }
        public Guid id { get; set; }
        public string destination { get; set; }
        public string hold { get; set; }
        public decimal holdQuantity { get; set; }
        public Guid marks_and_nos_name { get; set; }
        public Guid cargo_type_name { get; set; }
        public Guid receiver_name { get; set; }
        public string otherinfo { get; set; }

        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string _marks_and_nos_name { get; set; }
        public string _cargo_type_name { get; set; }
        public string _receiver_name { get; set; }
    }
}
