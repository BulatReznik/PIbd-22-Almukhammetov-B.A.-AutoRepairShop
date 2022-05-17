using Microsoft.EntityFrameworkCore;
using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using RepairDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairDatabaseImplement.Implements
{
    public class WareHouseStorage: IWareHouseStorage
    {
        public bool CheckWriteOff(CheckWriteOffBindingModel model)
        {
            using var context = new RepairDatabase();
            var neccesary = context.RepairComponents.Where(rec => rec.RepairId == model.RepairId).ToDictionary(rec => rec.ComponentId, rec => rec.Count * model.Count);
            using var transaction = context.Database.BeginTransaction();
            foreach (var key in neccesary.Keys)
            {
                foreach (var wareHouseComponent in context.WareHouseComponents.Where(rec => rec.ComponentId == key))
                {
                    if (wareHouseComponent.Count > neccesary[key])
                    {
                        wareHouseComponent.Count -= neccesary[key];
                        neccesary[key] = 0;
                        break;
                    }
                    else
                    {
                        neccesary[key] -= wareHouseComponent.Count;
                        wareHouseComponent.Count = 0;
                    }
                }
                if (neccesary[key] > 0)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            context.SaveChanges();
            transaction.Commit();
            return true;
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

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            var warehouse = context.WareHouses
            .Include(rec => rec.WareHouseComponents)
            .ThenInclude(rec => rec.Component)
            .FirstOrDefault(rec => rec.WareHouseName == model.WareHouseName ||rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            return context.WareHouses
            .Include(rec => rec.WareHouseComponents)
            .ThenInclude(rec => rec.Component)
            .Where(rec => rec.WareHouseName.Contains(model.WareHouseName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<WareHouseViewModel> GetFullList()
        {
            using var context = new RepairDatabase();
            return context.WareHouses
            .Include(rec => rec.WareHouseComponents)
            .ThenInclude(rec => rec.Component)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(WareHouseBindingModel model)
        {
            using var context = new RepairDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                WareHouse warehouse = new WareHouse()
                {
                    WareHouseName = model.WareHouseName,
                    ResponsibleName = model.ResponsibleName,
                    DateCreate = model.DateCreate
                };
                context.WareHouses.Add(warehouse);
                context.SaveChanges();
                CreateModel(model, warehouse, context);
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
        private static WareHouseViewModel CreateModel(WareHouse warehouse)
        {
            return new WareHouseViewModel
            {
                Id = warehouse.Id,
                WareHouseName = warehouse.WareHouseName,
                ResponsibleName = warehouse.ResponsibleName,
                DateCreate = warehouse.DateCreate,
                WareHouseComponents = warehouse.WareHouseComponents
            .ToDictionary(recII => recII.ComponentId, recII => (recII.Component?.ComponentName, recII.Count))
            };
        }

        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse,
        RepairDatabase context)
        {
            wareHouse.WareHouseName = model.WareHouseName;
            wareHouse.ResponsibleName = model.ResponsibleName;
            wareHouse.DateCreate = model.DateCreate;
            if (model.Id.HasValue)
            {
                var warehouseIngredients = context.WareHouseComponents.Where(rec =>
                rec.WareHouseId == model.Id.Value).ToList();
                context.WareHouseComponents.RemoveRange(warehouseIngredients.Where(rec => !model.WareHouseComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                foreach (var updateIngredient in warehouseIngredients)
                {
                    updateIngredient.Count = model.WareHouseComponents[updateIngredient.ComponentId].Item2;
                    model.WareHouseComponents.Remove(updateIngredient.ComponentId);
                }
                context.SaveChanges();
            }
            foreach (var wi in model.WareHouseComponents)
            {
                context.WareHouseComponents.Add(new WareHouseComponent
                {
                    WareHouseId = wareHouse.Id,
                    ComponentId = wi.Key,
                    Count = wi.Value.Item2,
                });
                context.SaveChanges();
            }
            return wareHouse;
        }
    }
}
