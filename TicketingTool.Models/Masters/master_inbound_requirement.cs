using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_inbound_requirement
    {
        public int? spType { get; set; }
        public string auto_id { get; set; }
        public Guid? requirement_type { get; set; }
        public Guid? vessel_name { get; set; }
        public Guid? discharge_port { get; set; }
        public string EAT_Date { get; set; }
        public string EAT_HH { get; set; }
        public string EAT_MM { get; set; }
        public string refNo { get; set; }
        public string rcn { get; set; }
        public string LoadPort { get; set; }
        public bool? is_active { get; set; }
        public Guid id { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string discharge_port_name { get; set; }
        public string vessel { get; set; }
        public string mastername { get; set; }
        public string mastercontactnumber { get; set; }
        public string masteremail { get; set; }
        public Guid owners_name { get; set; }
        public Guid receiver_name { get; set; }
        public Guid surveyor_name { get; set; }
        public Guid stevedore_name { get; set; }
        public Guid shipping_name { get; set; }
        public Guid checker_name { get; set; }
        public string VoyageNo { get; set; }
        public string LastPortofCall { get; set; }
        public Guid FC_Stevedore_Name { get; set; }
        public string flags { get; set; }
        public string ComapnyName { get; set; }
        public string TotalQuantity { get; set; }
        public bool? is_approved { get; set; }
        public string eta_notice { get; set; }
        public int EAT_HH_eta_notice { get; set; }
        public int EAT_MM_eta_notice { get; set; }
        public int EAT_HH_commence_to_discharge_cargo { get; set; }
        public string commence_to_discharge_cargo { get; set; }
        public int EAT_MM_commence_to_discharge_cargo { get; set; }
        public string est_to_complete_loading_cargo { get; set; }
        public string plan_of_sailing { get; set; }
        public int vessels_stay { get; set; }
        public string lat { get; set; }
        public string longt { get; set; }
        public string nor_notice { get; set; }

        public int EAT_HH_nor_notice { get; set; }
        public int EAT_MM_nor_notice { get; set; }
    }
}
