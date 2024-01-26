using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingTool.Models
{
    public class vm_Report_InBound_Details
    {
        public Guid id { get; set; }
        public string auto_id { get; set; }
        public Guid id_ref { get; set; }
        public string CompanyName { get; set; }
        public string requirement_type { get; set; }
        public string RefNo { get; set; }
        public string Rcn { get; set; }
        public string TypeofCargo { get; set; }
        public decimal? Quantity { get; set; }
        public string LoadPort { get; set; }
        public string DischargePort { get; set; }
        public string EAT_Date { get; set; }
        public string EAT_Time { get; set; }
        public string PersonInCharge { get; set; }
        public string approval_number { get; set; }
        public string approved_by { get; set; }
        public bool? is_approved { get; set; }
        public DateTime? approved_date { get; set; }
        public DateTime? created_date { get; set; }
        public string CompanyEmail { get; set; }
    }
}