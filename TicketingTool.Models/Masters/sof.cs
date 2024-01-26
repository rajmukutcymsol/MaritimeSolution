using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class sof
    {
        public string auto_id { get; set; }
        public int? spType { get; set; }
        public Guid id { get; set; }
        public Guid id_ref { get; set; }
        public string departure_from_port { get; set; }
        public string departure_date { get; set; }
        public int departure_date_HH { get; set; }
        public int departure_date_MM { get; set; }
        public string ETA_Next_Port_Name { get; set; }
        public string ETA_Next_Port_Date { get; set; }
        public int ETA_Next_Port_MM { get; set; }
        public int ETA_Next_Port_HH { get; set; }
        public string ETA_Next_Port_AMPM { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string created_date { get; set; }
        public bool approved { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
    }
}
