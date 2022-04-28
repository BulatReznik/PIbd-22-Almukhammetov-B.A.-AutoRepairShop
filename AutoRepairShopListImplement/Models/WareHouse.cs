using System;
using System.Collections.Generic;
using System.Text;

namespace RepairListImplement.Models
{
    public class WareHouse
    {
        public int Id { get; set; }
        public string WareHouseName { get; set; }
        public string ResponsibleName { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> WareHouseComponents { get; set; }
    }
}
