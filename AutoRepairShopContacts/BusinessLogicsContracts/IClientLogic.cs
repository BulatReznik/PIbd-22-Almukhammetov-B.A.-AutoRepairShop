using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepairContracts.BusinessLogicsContracts
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
