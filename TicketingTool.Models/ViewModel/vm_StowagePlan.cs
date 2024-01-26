using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_StowagePlan
    {
        public string auto_id { get; set; }
        public string vesselname { get; set; }
        public string loabeamdeapth { get; set; }
        public string capacities { get; set; }
        public string deadweight { get; set; }
        public string arrival_date { get; set; }
        public string sailedon_date { get; set; }
        public string destination { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public decimal gross_weight_of_cargo { get; set; }
    }
   
}
