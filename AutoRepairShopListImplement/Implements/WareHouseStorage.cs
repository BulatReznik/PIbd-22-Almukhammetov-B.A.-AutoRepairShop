using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using RepairListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepairListImplement.Implements
{
    class WareHouseStorage : IWareHouseStorage
    {
        private readonly DataListSingleton source;
        public WareHouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<WareHouseViewModel> GetFullList()
        {
            List<WareHouseViewModel> result = new List<WareHouseViewModel>();
            foreach (var component in source.WareHouse)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<WareHouseViewModel> result = new List<WareHouseViewModel>();
            foreach (var WareHouse in source.WareHouse)
            {
                if (WareHouse.WareHouseName.Contains(model.WareHouseName))
                {
                    result.Add(CreateModel(WareHouse));
                }
            }
            return result;
        }
        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var wareHouse in source.WareHouse)
            {
                if (wareHouse.Id == model.Id || wareHouse.WareHouseName == model.WareHouseName)
                {
                    return CreateModel(wareHouse);
                }
            }
            return null;
        }
        public void Insert(WareHouseBindingModel model)
        {
            WareHouse tempWareHouse = new WareHouse { Id = 1, WareHouseComponents = new Dictionary<int, (string, int)>() };
            foreach (var WareHouse in source.WareHouse)
            {
                if (WareHouse.Id >= tempWareHouse.Id)
                {
                    tempWareHouse.Id = WareHouse.Id + 1;
                }
            }
            source.WareHouse.Add(CreateModel(model, tempWareHouse));
        }
        public void Update(WareHouseBindingModel model)
        {
            WareHouse tempWareHouse = null;
            foreach (var WareHouse in source.WareHouse)
            {
                if (WareHouse.Id == model.Id)
                {
                    tempWareHouse = WareHouse;
                }
            }
            if (tempWareHouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWareHouse);
        }
        public void Delete(WareHouseBindingModel model)
        {
            for (int i = 0; i < source.WareHouse.Count; ++i)
            {
                if (source.WareHouse[i].Id == model.Id)
                {
                    source.WareHouse.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
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
                    wareHouse.WareHouseComponents[component.Key] = model.WareHouseComponents[component.Key];
                }
                else
                {
                    wareHouse.WareHouseComponents.Add(component.Key, model.WareHouseComponents[component.Key]);
                }
            }
            return wareHouse;
        }
        private WareHouseViewModel CreateModel(WareHouse WareHouse)
        {
            Dictionary<int, (string, int)> WareHouseComponents = new Dictionary<int, (string, int)>();
            foreach (var sc in WareHouse.WareHouseComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (sc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                WareHouseComponents.Add(sc.Key, (componentName, sc.Value.Item2));
            }
            return new WareHouseViewModel
            {
                Id = WareHouse.Id,
                WareHouseName = WareHouse.WareHouseName,
                ResponsibleName = WareHouse.ResponsibleName,
                DateCreation = WareHouse.DateCreate,
                WareHouseComponents = WareHouseComponents
            };
        }
    }
}