using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepairContracts.BusinessLogicsContracts
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);
        void CreateOrUpdate(ImplementerBindingModel model);
        void Delete(ImplementerBindingModel model);
    }
}
