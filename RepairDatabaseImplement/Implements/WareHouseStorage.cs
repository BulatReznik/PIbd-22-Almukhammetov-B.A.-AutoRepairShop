using Microsoft.EntityFrameworkCore;
using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using RepairDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDatabaseImplement.Implements
{
    public class WareHouseStorage: IWareHouseStorage
    {
        public List<WareHouseViewModel> GetFullList()
        {
            using var context = new RepairDatabase();
            return context.WareHouses.Include(rec => rec.WareHouseComponents).ThenInclude(rec => rec.Component).ToList().Select(CreateModel).ToList();
        }
        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            return context.WareHouses.Include(rec => rec.WareHouseComponents).ThenInclude(rec => rec.Component).Where(rec => rec.WareHouseName.Contains(model.WareHouseName)).ToList().Select(CreateModel).ToList();
        }
        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            var wareHouse = context.WareHouses.Include(rec => rec.WareHouseComponents).ThenInclude(rec => rec.Component).FirstOrDefault(rec => rec.WareHouseName == model.WareHouseName || rec.Id == model.Id);
            return wareHouse != null ? CreateModel(wareHouse) : null;
        }
        public void Insert(WareHouseBindingModel model)
        {
            using var context = new RepairDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                WareHouse wareHouse = new WareHouse()
                {
                    WareHouseName = model.WareHouseName,
                    ResponsibleName = model.ResponsibleName,
                    DateCreate = model.DateCreate
                };
                context.WareHouses.Add(wareHouse);
                context.SaveChanges();
                CreateModel(model, wareHouse, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(WareHouseBindingModel model)
        {
            using var context = new RepairDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(WareHouseBindingModel model)
        {
            using var context = new RepairDatabase();
            WareHouse element = context.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.WareHouses.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse, RepairDatabase context)
        {
            wareHouse.WareHouseName = model.WareHouseName;
            wareHouse.ResponsibleName = model.ResponsibleName;
            wareHouse.DateCreate = model.DateCreate;
            if (model.Id.HasValue)
            {
                var wareHouseComponents = context.WareHouseComponents.Where(rec => rec.WareHouseId == model.Id.Value).ToList();
                context.WareHouseComponents.RemoveRange(wareHouseComponents.Where(rec => !model.WareHouseComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                foreach (var updateComponent in wareHouseComponents)
                {
                    updateComponent.Count = model.WareHouseComponents[updateComponent.ComponentId].Item2;
                    model.WareHouseComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.WareHouseComponents)
            {
                context.WareHouseComponents.Add(new WareHouseComponent
                {
                    WareHouseId = wareHouse.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return wareHouse;
        }
        private static WareHouseViewModel CreateModel(WareHouse wareHouse)
        {
            return new WareHouseViewModel
            {
                Id = wareHouse.Id,
                WareHouseName = wareHouse.WareHouseName,
                ResponsibleName = wareHouse.ResponsibleName,
                DateCreate = wareHouse.DateCreate,
                WareHouseComponents = wareHouse.WareHouseComponents.ToDictionary(recPC => recPC.ComponentId, recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
        public bool CheckWriteOff(Dictionary<int, int> components)
        {
            Dictionary<int, int> stockComponents = new Dictionary<int, int>();
            using var context = new RepairDatabase();
            foreach (var wareHouse in context.WareHouses)
            {
                foreach (var comp in wareHouse.WareHouseComponents)
                {
                    if (stockComponents.ContainsKey(comp.ComponentId))
                    {
                        stockComponents[comp.ComponentId] += comp.Count;
                    }
                    else
                    {
                        stockComponents.Add(comp.ComponentId, comp.Count);
                    }
                }
            }
            foreach (var comp in components)
            {
                if (!stockComponents.ContainsKey(comp.Key) || stockComponents[comp.Key] < comp.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public bool WriteOffBalance(Dictionary<int, int> components)
        {
            using var context = new RepairDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Dictionary<int, int> remainComponents = components.ToDictionary(comp => comp.Key, comp => comp.Value);
                foreach (WareHouseComponent wareHouseComponent in context.WareHouseComponents)
                {
                    if (remainComponents.ContainsKey(wareHouseComponent.ComponentId))
                    {
                        int decommission = Math.Min(remainComponents[wareHouseComponent.ComponentId], wareHouseComponent.Count);
                        remainComponents[wareHouseComponent.ComponentId] -= decommission;
                        wareHouseComponent.Count -= decommission;
                        if (remainComponents[wareHouseComponent.ComponentId] == 0)
                        {
                            remainComponents.Remove(wareHouseComponent.ComponentId);
                        }
                    }
                }
                if (remainComponents.Count != 0)
                {
                    throw new Exception("На складах недостаточно компонентов");
                }
                context.SaveChanges();
                context.WareHouseComponents.RemoveRange(context.WareHouseComponents.Where(rec => rec.Count == 0).ToList());
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            return true;
        }
    }
}
