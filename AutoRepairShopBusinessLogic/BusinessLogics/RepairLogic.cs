using RepairContracts.BindingModels;
using RepairContracts.BusinessLogicsContracts;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace RepairBusinessLogic.BusinessLogics
{
    public class RepairLogic : IRepairLogic
    {
        private readonly IRepairStorage _repairStorage;

        public RepairLogic(IRepairStorage repairStorage)
        {
            _repairStorage = repairStorage;
        }

        public List<RepairViewModel> Read(RepairBindingModel model)
        {
            if (model == null)
            {
                return _repairStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<RepairViewModel> { _repairStorage.GetElement(model) };
            }
            return _repairStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(RepairBindingModel model)
        {
            var element = _repairStorage.GetElement(new RepairBindingModel
            {
                RepairName = model.RepairName,
                Price = model.Price,
                RepairComponents = model.RepairComponents
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть ремонтные работы с таким названием");
            }
            if (model.Id.HasValue)
            {
                _repairStorage.Update(model);
            }
            else
            {
                _repairStorage.Insert(model);
            }
        }

        public void Delete(RepairBindingModel model)
        {
            var element = _repairStorage.GetElement(new RepairBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Ремонтные работы не найденЫ");
            }
            _repairStorage.Delete(model);
        }
    }
}

