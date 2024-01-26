using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_inbound_manifest
    {
        public Guid id { get; set; }
        public string auto_id { get; set; }
        public string blno { get; set; }
        public Guid shipper_name { get; set; }
        public string consignee { get; set; }
        public Guid notify { get; set; }
        public Guid marks_and_nos_name { get; set; }
        public Guid quantity_and_kind_of_cargo_name { get; set; }
        public Guid quantity_and_kind_of_cargo { get; set; }

        public decimal? quantity { get; set; }
        public string cargotype_desc { get; set; }
        public bool? is_active { get; set; }
        public bool? is_deleted { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string shippername { get; set; }
        public string consignee_name { get; set; }
        public string notify_name { get; set; }
        public string marks_and_nos { get; set; }
        public bool? is_cleanonBoard { get; set; }
        public Guid qtt_name { get; set; }
        public Guid cargo_type_name { get; set; }
        public string Quantity_and_Kind_of_for_cargo { get; set; }
        public string quantity_each { get; set; }
        public string eta_notice { get; set; }
        public int EAT_HH_eta_notice { get; set; }
        public int EAT_MM_eta_notice { get; set; }
        public int EAT_HH_commence_to_discharge_cargo { get; set; }
        public int commence_to_discharge_cargo { get; set; }
        public int EAT_MM_commence_to_discharge_cargo { get; set; }
        public string est_to_complete_loading_cargo { get; set; }
        public string plan_of_sailing { get; set; }
        public int vessels_stay { get; set; }
        //
    }
}
