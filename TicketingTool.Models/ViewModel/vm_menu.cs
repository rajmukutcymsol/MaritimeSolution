﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.ViewModel
{
    public class vm_menu
    {
        public Guid? id { get; set; }
        public string menu_name { get; set; }
        public string menu_controller { get; set; }
        public string menu_action { get; set; }
        public string menu_icon { get; set; }
        public string menu_class { get; set; }
        public Guid? parent_id { get; set; }
        
    }
}