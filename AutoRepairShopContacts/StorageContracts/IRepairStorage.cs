using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepairContracts.StorageContracts
{
    public interface IRepairStorage
    {
        List<RepairViewModel> GetFullList();
        List<RepairViewModel> GetFilteredList(RepairBindingModel model);
        RepairViewModel GetElement(RepairBindingModel model);
        void Insert(RepairBindingModel model);
        void Update(RepairBindingModel model);
        void Delete(RepairBindingModel model);
    }

}
