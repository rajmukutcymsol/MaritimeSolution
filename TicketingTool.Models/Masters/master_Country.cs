using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public bool is_active { get; set; }
    }
}
