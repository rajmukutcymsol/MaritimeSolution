﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_email_group
    {
        public Guid? id { get; set; }
        public string group_name { get; set; }
        public bool is_active { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }


    }
}
