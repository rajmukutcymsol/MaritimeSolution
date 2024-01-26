using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_State
    {
        public int stateid { get; set; }
        public Guid id { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
        public bool is_active { get; set; }
        public string country_name { get; set; }
        public string state_name { get; set; }


    }
}
