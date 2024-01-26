using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Masters;

namespace TicketingTool.Models.ViewModel
{
    public class vw_user_projects_mapping
    {
        public List<vw_user_projects_mapping> vw_User_Projects_Mapping { get; set; }
        public List<master_project> master_Projects { get; set; }
        public Guid projectid { get; set; }
        public Guid id { get; set; }
        public string employee_id { get; set; }
        public string project_name { get; set; }


    }
}
