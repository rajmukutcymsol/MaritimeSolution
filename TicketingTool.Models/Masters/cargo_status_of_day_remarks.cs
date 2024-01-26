using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class cargo_status_of_day_remarks
    {
        public Guid id { get; set; }
        public string date_of_action { get; set; }
        public string report_date { get; set; }
        public int? HH_date_of_action { get; set; }
        public int? MM_date_of_action { get; set; }
        public int? HH_report_date { get; set; }
        public int? MM_report_date { get; set; }
        public string remarks_comments { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
        public string auto_id { get; set; }
        public Guid? id_ref { get; set; }
        public int? to_HH_date_of_action { get; set; }
        public int? to_MM_date_of_action { get; set; }


        // for 
        public string loading_text { get; set; }
        public string loading_text_date { get; set; }
        public int from_HH_loading_text { get; set; }
        public int to_MM_loading_text { get; set; }
        public string loading_text_until { get; set; }

        // 

        public string comp_loading { get; set; }
        public int from_HH_comp_loading { get; set; }
        public int from_MM_comp_loading { get; set; }
        public string to_comp_loading { get; set; }
        public int to_HH_comp_loading { get; set; }
        public int to_MM_comp_loading { get; set; }

        //
        public string comm_loading { get; set; }
        public int from_HH_comm_loading { get; set; }
        public int from_MM_comm_loading { get; set; }
        public string to_comm_loading { get; set; }
        public int to_HH_comm_loading { get; set; }
        public int to_MM_comm_loading { get; set; }

        //
        public decimal hold_1_total { get; set; }
        public decimal hold_2_total { get; set; }
        public decimal hold_3_total { get; set; }
        public decimal hold_4_total { get; set; }
        public decimal hold_5_total { get; set; }

        //
        public int from_HH_daytime { get; set; }
        public int from_MM_daytime { get; set; }
        public int to_HH_daytime { get; set; }
        public int to_MM_daytime { get; set; }
        //

        public int gang_daytime { get; set; }
        public decimal daytime_hold1_out { get; set; }
        public decimal daytime_hold2_out { get; set; }
        public decimal daytime_hold3_out { get; set; }
        public decimal daytime_hold4_out { get; set; }
        public decimal daytime_hold5_out { get; set; }
        public decimal daytime_total_out { get; set; }

        //
        public int from_HH_first { get; set; }
        public int from_MM_first { get; set; }
        public int to_HH_first { get; set; }
        public int to_MM_first { get; set; }

        //
        public int gang_first { get; set; }
        public decimal first_hold1_out { get; set; }
        public decimal first_hold2_out { get; set; }
        public decimal first_hold3_out { get; set; }
        public decimal first_hold4_out { get; set; }
        public decimal first_hold5_out { get; set; }
        public decimal first_total_out { get; set; }

        //
        public int from_HH_second { get; set; }
        public int from_MM_second { get; set; }
        public int to_HH_second { get; set; }
        public int to_MM_second { get; set; }

        //
        public int gang_second { get; set; }
        public decimal second_hold1_out { get; set; }
        public decimal second_hold2_out { get; set; }
        public decimal second_hold3_out { get; set; }
        public decimal second_hold4_out { get; set; }
        public decimal second_hold5_out { get; set; }
        public decimal second_total_out { get; set; }

        //
        public int gang_total { get; set; }
        public decimal total_hold1_out { get; set; }
        public decimal total_hold2_out { get; set; }
        public decimal total_hold3_out { get; set; }
        public decimal total_hold4_out { get; set; }
        public decimal total_hold5_out { get; set; }
        public decimal total_total { get; set; }

        //
        public int gang_previous { get; set; }
        public decimal previous_hold1_out { get; set; }
        public decimal previous_hold2_out { get; set; }
        public decimal previous_hold3_out { get; set; }
        public decimal previous_hold4_out { get; set; }
        public decimal previous_hold5_out { get; set; }
        public decimal previous_total { get; set; }

        //
        public int gang_grand_total { get; set; }
        public decimal grand_hold1_out { get; set; }
        public decimal grand_hold2_out { get; set; }
        public decimal grand_hold3_out { get; set; }
        public decimal grand_hold4_out { get; set; }
        public decimal grand_hold5_out { get; set; }
        public decimal grand_total { get; set; }

        //
        public decimal balance_cargo_hold1 { get; set; }
        public decimal balance_cargo_hold2 { get; set; }
        public decimal balance_cargo_hold3 { get; set; }
        public decimal balance_cargo_hold4 { get; set; }
        public decimal balance_cargo_hold5 { get; set; }
        public decimal balance_total { get; set; }

        //
        public string working_time { get; set; }
        public string working_through { get; set; }

        //
        

    }
}
