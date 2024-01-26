using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_vessel
    {
        public Guid id { get; set; }
        public string vesselName { get; set; }
        public string vesselCode { get; set; }
        public string deadweight { get; set; }
        public string loa { get; set; }
        public string maxDraft { get; set; }
        public string beam { get; set; }
        public string grt { get; set; }
        public string nrt { get; set; }
        public string flags { get; set; }
        public string callSign { get; set; }
        public string imoNumber { get; set; }
        public string hatchHolds { get; set; }
        public string swl { get; set; }
        public string vesselOthers { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string LastPortofcall { get; set; }
        public string piclub { get; set; }
        public string ClassificationSociety { get; set; }
        public string Depth { get; set; }
    }
}
