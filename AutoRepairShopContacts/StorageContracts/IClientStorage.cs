using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using System.Collections.Generic;

namespace RepairContracts.StorageContracts
{
    public interface IClientStorage
    {
        List<ClientViewModel> GetFullList();
        List<ClientViewModel> GetFilteredList(ClientBindingModel model);
        ClientViewModel GetElement(ClientBindingModel model);
        void Insert(ClientBindingModel model);
        void Update(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
