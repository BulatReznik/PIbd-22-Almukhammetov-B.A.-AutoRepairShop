using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using RepairFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairFileImplement.Implements
{
    public class WareHouseStorage: IWareHouseStorage
    {
        private readonly FileDataListSingleton source;

        public WareHouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<WareHouseViewModel> GetFullList()
        {
            return source.WareHouses.Select(CreateModel).ToList();
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.WareHouses.Where(rec => rec.WareHouseName.Contains(model.WareHouseName)).Select(CreateModel).ToList();
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var wareHouse = source.WareHouses.FirstOrDefault(rec => rec.WareHouseName == model.WareHouseName || rec.Id == model.Id);
            return wareHouse != null ? CreateModel(wareHouse) : null;
        }

        public void Insert(WareHouseBindingModel model)
        {
            int maxId = source.WareHouses.Count > 0 ? source.WareHouses.Max(rec => rec.Id) : 0;
            var element = new WareHouse { Id = maxId + 1, WareHouseComponents = new Dictionary<int,int>() };
            source.WareHouses.Add(CreateModel(model, element));
        }

        public void Update(WareHouseBindingModel model)
        {
            var element = source.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        public void Delete(WareHouseBindingModel model)
        {
            WareHouse element = source.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.WareHouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public bool CheckWriteOff(CheckWriteOffBindingModel model)
        {
            var list = GetFullList();
            var neccesary = new Dictionary<int, int>(source.Repairs.FirstOrDefault(rec => rec.Id == model.RepairId).RepairComponents);
            var available = new Dictionary<int, int>();
            neccesary.ToDictionary(kvp => neccesary[kvp.Key] *= model.Count);
            foreach (var wareHouse in list)
            {
                foreach (var component in wareHouse.WareHouseComponents)
                {
                    if (available.ContainsKey(component.Key))
                    {
                        available[component.Key] += component.Value.Item2;
                    }
                    else
                    {
                        available.Add(component.Key, component.Value.Item2);
                    }
                }
            }
            bool can = available.ToList().All(component => component.Value >= neccesary[component.Key]);
            if (!can || available.Count == 0)
            {
                return false;
            }

            foreach (var wareHouse in list)
            {
                var wareHouseComponents = wareHouse.WareHouseComponents;
                foreach (var key in wareHouse.WareHouseComponents.Keys)
                {
                    var value = wareHouse.WareHouseComponents[key];
                    if (neccesary.ContainsKey(key))
                    {
                        if (value.Item2 > neccesary[key])
                        {
                            wareHouseComponents[key] = (value.Item1, value.Item2 - neccesary[key]);
                            neccesary[key] = 0;
                        }
                        else
                        {
                            wareHouseComponents[key] = (value.Item1, 0);
                            neccesary[key] -= value.Item2;
                        }
                        Update(new WareHouseBindingModel
                        {
                            Id = wareHouse.Id,
                            WareHouseName = wareHouse.WareHouseName,
                            ResponsibleName=wareHouse.ResponsibleName,
                            DateCreate = wareHouse.DateCreate,
                            WareHouseComponents = wareHouseComponents
                        });
                    }
                }
            }
            return true;
        }

        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse)
        {
            wareHouse.WareHouseName = model.WareHouseName;
            wareHouse.ResponsibleName = model.ResponsibleName;
            wareHouse.DateCreate = model.DateCreate;
            foreach (var key in wareHouse.WareHouseComponents.Keys.ToList())
            {
                if (!model.WareHouseComponents.ContainsKey(key))
                {
                    wareHouse.WareHouseComponents.Remove(key);
                }
            }
            foreach (var component in model.WareHouseComponents)
            {
                if (wareHouse.WareHouseComponents.ContainsKey(component.Key))
                {
                    wareHouse.WareHouseComponents[component.Key] = model.WareHouseComponents[component.Key].Item2;
                }
                else
                {
                    wareHouse.WareHouseComponents.Add(component.Key, model.WareHouseComponents[component.Key].Item2);
                }
            }
            return wareHouse;
        }

        private WareHouseViewModel CreateModel(WareHouse wareHouse)
        {
            return new WareHouseViewModel
            {
                Id = wareHouse.Id,
                WareHouseName = wareHouse.WareHouseName,
                ResponsibleName = wareHouse.ResponsibleName,
                DateCreate = wareHouse.DateCreate,
                WareHouseComponents = wareHouse.WareHouseComponents
                    .ToDictionary(recPC => recPC.Key, recPC =>
                    (source.Components.FirstOrDefault(recC => recC.Id == recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}
