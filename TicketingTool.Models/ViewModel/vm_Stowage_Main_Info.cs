using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_Stowage_Main_Info
    {
        public string auto_id { get; set; }
        public Guid id_ref { get; set; }
        public Guid id { get; set; }
        public string vesselname { get; set; }
        public string loabeamdeapth { get; set; }
        public string capacities { get; set; }
        public string deadweight { get; set; }
        public string arrival_date { get; set; }
        public string sailedon_date { get; set; }
        public string gross_weight_of_cargo { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public bool approved { get; set; }
    }
}
