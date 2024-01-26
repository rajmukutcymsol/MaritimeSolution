using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_ArrivalCondition
    {
        public string auto_id { get; set; }
        public Guid id_ref { get; set; }
        public Guid id { get; set; }

        public Guid vesselname { get; set; }
        public string vessel_name { get; set; }

        public string flags { get; set; }
        public Guid type_of_cargo { get; set; }
        public string gross_weight_of_cargo { get; set; }
        public Guid qtt_name { get; set; }
        public string readiness_tendered { get; set; }
        public string readiness_accepted { get; set; }
        public string arrival_at { get; set; }
        public string pilot_on_board_arrival { get; set; }
        public string dropped_anchor { get; set; }
        public string draft_on_arrival { get; set; }
        public string bunker_rob_on_arrival { get; set; }
        public string commenced_discharge_cargo { get; set; }
        public string completed_discharge_cargo { get; set; }
        public string lat { get; set; }
        public string longt { get; set; }
        
        public string pilot_on_board_departure { get; set; }
        public string departue_from { get; set; }
        public string draft_on_departure { get; set; }
        public string bunker_rob_on_departure { get; set; }
        public string bunker_fuel_oil { get; set; }
        public string bunker_diesel_oil { get; set; }
        public string bunker_fresh_water { get; set; }
        public string bunker_eta_next_port { get; set; }
        public string other_watch_man { get; set; }
        public string other_police_man { get; set; }
        public string other_cash_advance { get; set; }

        public string EAT_HH_pilot_on_board_arrival { get; set; }
        public string EAT_MM_pilot_on_board_arrival { get; set; }
        public string EAT_HH_dropped_anchor { get; set; }
        public string EAT_MM_dropped_anchor { get; set; }
        public string EAT_HH_commenced_discharge_cargo { get; set; }
        public string EAT_MM_commenced_discharge_cargo { get; set; }

        public string EAT_HH_completed_discharge_cargo { get; set; }
        public string EAT_MM_completed_discharge_cargo { get; set; }
        public string EAT_HH_pilot_on_board_departure { get; set; }
        public string EAT_MM_pilot_on_board_departure { get; set; }
        public string EAT_HH_departure_from { get; set; }
        public string EAT_MM_departure_from { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string created_date { get; set; }
        public bool approved { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public string nor_notice { get; set; }
        public string discharge_port_name { get; set; }

        //
        public string fwd { get; set; }
        public string aft { get; set; }
        public string fo { get; set; }
        public string doo { get; set; }
        public string fw { get; set; }
        public string departue_from_port_name { get; set; }

        //
        public string ETA_Next_Port_Date { get; set; }
        public int ETA_Next_Port_MM { get; set; }
        public int ETA_Next_Port_HH { get; set; }
        public int ETA_Next_Port_AMPM { get; set; }



    }
}
