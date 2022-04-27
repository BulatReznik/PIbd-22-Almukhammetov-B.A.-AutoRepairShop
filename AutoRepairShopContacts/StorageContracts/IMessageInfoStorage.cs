using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using System.Collections.Generic;

namespace RepairContracts.StorageContracts
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();
        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        void Insert(MessageInfoBindingModel model);
    }
}
