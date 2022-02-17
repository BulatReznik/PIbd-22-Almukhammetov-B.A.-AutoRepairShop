using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepairContracts.StorageContracts
{
    public interface IWareHouseStorage
	{
		List<WareHouseViewModel> GetFullList();
		List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model);
		WareHouseViewModel GetElement(WareHouseBindingModel model);
		void Insert(WareHouseBindingModel model);
		void Update(WareHouseBindingModel model);
		void Delete(WareHouseBindingModel model);
	}
}