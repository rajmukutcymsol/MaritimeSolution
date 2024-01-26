using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_common_mstr
    {
        public Guid? id { get; set; }
        public string CompanyName { get; set; }
        public string personincharge { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }
        public int stateid { get; set; }
        public int cityid { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string CategoryName { get; set; }
        public Guid? MastersCategoryID { get; set; }
        public bool? is_active { get; set; }
        public bool? is_deleted { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public int auto_id { get; set; }

    }
}
