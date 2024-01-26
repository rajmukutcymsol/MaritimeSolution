using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class CargoStatusOfDay
    {
        public Guid id { get; set; }
        public string daytime { get; set; }
        public string firstHN { get; set; }
        public string secondHN { get; set; }
        public int? from_HH_cargo_status_daytime { get; set; }
        public int? to_MM_cargo_status_daytime { get; set; }
        public int? from_HH_cargo_status_first { get; set; }
        public int? to_MM_cargo_status_first { get; set; }
        public string gang { get; set; }
        public string hold { get; set; }
        public decimal total_out { get; set; }

        public string created_by { get; set; }
        public string updated_by { get; set; }
        public bool? is_active { get; set; }
        public bool? is_deleted { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
        public string selectTime { get; set; }
        public string auto_id { get; set; }
        public Guid id_ref { get; set; }
        public string main { get; set; }
        public string report_date { get; set; }
        public int HH_report_date { get; set; }
        public int MM_report_date { get; set; }
    }
}
