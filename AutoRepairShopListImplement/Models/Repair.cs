using System.Collections.Generic;

namespace RepairListImplement.Models
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Repair
    {
        public int Id { get; set; }
        public string RepairName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> RepairComponents { get; set; }
    }
}
