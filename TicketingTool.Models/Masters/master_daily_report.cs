using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_daily_report:cargo_status_of_day_remarks
    {
        public string vessel_name { get; set; }
        public string flags { get; set; }
        public string discharge_port_name { get; set; }
        public string lat { get; set; }
        public string longt { get; set; }
        public string weather { get; set; }
        public string report_date { get; set; }
        public int HH_report_date { get; set; }
        public int MM_report_date { get; set; }
        public string from_daily_discharge_cargo { get; set; }
        public int from_MM_daily_discharge_cargo { get; set; }
        public int from_HH_daily_discharge_cargo { get; set; }

        public string to_daily_discharge_cargo { get; set; }
        public int to_HH_daily_discharge_cargo { get; set; }
        public int to_MM_daily_discharge_cargo { get; set; }
        public string fwt { get; set; }
        public string ft { get; set; }
        public string fo { get; set; }
        public string doo { get; set; }
        public string fw { get; set; }


        public Guid id { get; set; }
        public Guid id_ref { get; set; }
        public int? spType { get; set; }
        public string auto_id { get; set; }
        public bool? is_active { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public bool? is_approved { get; set; }

        // hold
        public decimal Hold1 { get; set; }
        public decimal Hold2 { get; set; }
        public decimal Hold3 { get; set; }
        public decimal Hold4 { get; set; }
        public decimal Hold5 { get; set; }
        public decimal TotalHold { get; set; }

    }
}
