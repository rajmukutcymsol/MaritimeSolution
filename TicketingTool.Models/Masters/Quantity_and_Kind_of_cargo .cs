﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class Quantity_and_Kind_of_cargo
    {
        public Guid id { get; set; }
        public string Quantity_and_Kind_of_cargo_name { get; set; }
        public bool is_active { get; set; }
    }
}