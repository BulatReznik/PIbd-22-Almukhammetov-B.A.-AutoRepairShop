using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairContracts.BindingModels
{
    public class ReplenishBindingModel
    {
        public int WareHouseId { get; set; }
        public int ComponentId { get; set; }
        public int Count { get; set; }

    }
}
