using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_pic_information
    {
    public Guid? id { get; set; }
    public string PicName { get; set; }
    public string PicDesignation { get; set; }
    public string PicPhone { get; set; }
    public string PicEmail { get; set; }
    public Guid? MastersCategoryID { get; set; }
    public bool? is_active { get; set; }
    public bool? Is_Deleted { get; set; }
    public string Created_By { get; set; }
    public DateTime? Created_Date { get; set; }
    public string Updated_By { get; set; }
    public DateTime? Updated_Date { get; set; }
        public int auto_id { get; set; }
    }
}
