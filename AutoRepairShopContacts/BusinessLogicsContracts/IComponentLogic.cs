using System;
using System.Collections.Generic;
using System.Text;
using RepairContracts.ViewModels;
using RepairContracts.BindingModels;


namespace RepairContracts.BusinessLogicsContracts
{
    public interface IComponentLogic
    {
        List<ComponentViewModel> Read(ComponentBindingModel model);
        void CreateOrUpdate(ComponentBindingModel model);
        void Delete(ComponentBindingModel model);
    }
}
