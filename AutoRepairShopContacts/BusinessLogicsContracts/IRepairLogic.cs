using System;
using System.Collections.Generic;
using System.Text;
using RepairContracts.ViewModels;
using RepairContracts.BindingModels;

namespace RepairContracts.BusinessLogicsContracts
{
    public interface IRepairLogic
    {
        List<RepairViewModel> Read(RepairBindingModel model);
        void CreateOrUpdate(RepairBindingModel model);
        void Delete(RepairBindingModel model);
    }
}
