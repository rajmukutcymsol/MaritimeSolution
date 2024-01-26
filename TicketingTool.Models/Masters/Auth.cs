using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class Auth
    {
        public Guid id { get; set; }
        public Guid RoleID { get; set; }
        public bool CanView { get; set; }
        public bool IsDownload { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public bool CanVerify { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public bool created_by { get; set; }
        public bool updated_by { get; set; }
    }
}
