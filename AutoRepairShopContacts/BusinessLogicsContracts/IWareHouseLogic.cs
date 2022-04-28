using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairContracts.BusinessLogicsContracts
{
    public interface IWareHouseLogic
    {
        List<WareHouseViewModel> Read(WareHouseBindingModel model);
        void CreateOrUpdate(WareHouseBindingModel model);
        void Delete(WareHouseBindingModel model);
        void AddComponent(WareHouseBindingModel model, int componentId, int Count);

    }
}
