﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDatabaseImplement.Models
{
    public class WareHouseComponent
    {
        public int Id { get; set; }
        public int WareHouseId { get; set; }
        public int ComponentId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
