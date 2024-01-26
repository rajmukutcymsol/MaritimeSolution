using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_City
    {
        //cityid
        public int cityid { get; set; }
        public string CityName { get; set; }
        public string stateId { get; set; }
        public int CountryID { get; set; }
        public bool is_active { get; set; }
        public Guid id { get; set; }
        public string country_name { get; set; }
        public string state_name { get; set; }


    }
}
