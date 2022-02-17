using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;


namespace RepairBusinessLogic.BusinessLogics
{
    public class WareHouseLogic
	{
		private readonly IWareHouseStorage _wareHouseStorage;
		private readonly IComponentStorage _componentStorage;
		public WareHouseLogic(IWareHouseStorage wareHouseStorage, IComponentStorage componentStorage)
		{
			_wareHouseStorage = wareHouseStorage;
			_componentStorage = componentStorage;
		}
		public List<WareHouseViewModel> Read(WareHouseBindingModel model)
		{
			if (model == null)
			{
				return _wareHouseStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<WareHouseViewModel> { _wareHouseStorage.GetElement(model) };
			}
			return _wareHouseStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(WareHouseBindingModel model)
		{
			var element = _wareHouseStorage.GetElement(new WareHouseBindingModel { WareHouseName = model.WareHouseName });
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть склад с таким названием");

			}

			if (model.Id.HasValue)
			{
				_wareHouseStorage.Update(model);
			}
			else
			{
				_wareHouseStorage.Insert(model);
			}
		}
		public void Delete(WareHouseBindingModel model)
		{
			var element = _wareHouseStorage.GetElement(new WareHouseBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_wareHouseStorage.Delete(model);
		}
		public void Fill(WareHouseBindingModel model, int componentId, int count)
		{
			var wareHouse = _wareHouseStorage.GetElement(new WareHouseBindingModel { Id = model.Id });

			if (wareHouse.WareHouseComponents.ContainsKey(componentId))
			{
				wareHouse.WareHouseComponents[componentId] =
					(wareHouse.WareHouseComponents[componentId].Item1, wareHouse.WareHouseComponents[componentId].Item2 + count);
			}
			else
			{
				var component = _componentStorage.GetElement(new ComponentBindingModel
				{
					Id = componentId
				});
				if (component == null)
				{
					throw new Exception("Компонент не найден");
				}
				wareHouse.WareHouseComponents.Add(componentId, (component.ComponentName, count));
			}
			_wareHouseStorage.Update(new WareHouseBindingModel
			{
				Id = wareHouse.Id,
				WareHouseName = wareHouse.WareHouseName,
				ResponsibleName = wareHouse.ResponsibleName,
				DateCreate = wareHouse.DateCreation,
				WareHouseComponents = wareHouse.WareHouseComponents
			});
		}
	}
}
