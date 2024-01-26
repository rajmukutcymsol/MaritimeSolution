using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Models.ViewModel
{
    public class vm_master
    {
        public List<master_customer> customers { get; set; }
        public List<master_domain> domains { get; set; }
        public List<master_efficiency> efficiencies { get; set; }
        public List<master_function> functions { get; set; }
        public List<master_function_level> function_levels { get; set; }
        public List<master_node_type> node_types { get; set; }
        public List<master_priority> priorities { get; set; }
        public List<master_project> projects { get; set; }
        public List<master_region> regions { get; set; }
        public List<master_requirement> requirements { get; set; }
        public List<master_sdlc> sdlcs { get; set; }
        public List<master_status> statuses { get; set; }
        public List<master_technology> technologies { get; set; }
        public List<master_tool> tools { get; set; }
        public List<master_vendor> vendors { get; set; }
        public List<BooleanDropdown> boolean_dropdowns { get; set; }
        public List<master_usecase> usecases { get; set; }
        public List<master_project_manager> masterprojectmanager { get; set; }
        public List<master_solution_architect> mastersolutionarchitect { get; set; }
        public List<master_developer> masterdeveloper { get; set; }
        public List<master_resolution_category> masterresolutioncategory { get; set; }
        public List<master_cli_ui> mastercligui { get; set; }

        //
        public List<master_Country> mastercountry { get; set; }
        public List<master_State> masterstate { get; set; }
        public List<master_City> mastercity { get; set; }
        public List<master_category> mastertype { get; set; }
        public List<master_vessel> mastervessel { get; set; }
        public List<master_discharge_port> masterport { get; set; }
        public List<vm_Owners> masterowners { get; set; }
        public List<vm_Receiver> masterreciver { get; set; }
        public List<vm_Surveyor> mastersurveyor { get; set; }
        public List<vm_stevedore> masterstevedore { get; set; }
        public List<vm_shipping> vm_shipping { get; set; }
        public List<vm_checker> vm_checker { get; set; }
        public List<vm_shipper> vm_shipper { get; set; }
        public List<marks_and_nos> vm_marks_and_nos { get; set; }
        public List<Quantity_and_Kind_of_cargo> quantity_and_Kind_of_cargo { get; set; }
        public List<master_cargo_type> Master_cargo_type { get; set; }
         public List<master_quantity_cargo> Master_quantity_cargo { get; set; }
        public List<master_FC_Stevedore_Operations> FC_Stevedore_Name { get; set; }

    }
}
