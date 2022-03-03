using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using RepairDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RepairDatabaseImplement.Implements
{
    public class RepairStorage: IRepairStorage
    {
        public List<RepairViewModel> GetFullList()
        {
            using var context = new RepairDatabase();
            return context.Repairs.Include(rec => rec.RepairComponents).ThenInclude(rec => rec.Component).ToList().Select(CreateModel).ToList();
        }
        public List<RepairViewModel> GetFilteredList(RepairBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            return context.Repairs.Include(rec => rec.RepairComponents).ThenInclude(rec => rec.Component).Where(rec => rec.RepairName.Contains(model.RepairName)).ToList().Select(CreateModel).ToList();
        }
        public RepairViewModel GetElement(RepairBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            var repair = context.Repairs.Include(rec => rec.RepairComponents).ThenInclude(rec => rec.Component).FirstOrDefault(rec => rec.RepairName == model.RepairName || rec.Id == model.Id);
            return repair != null ? CreateModel(repair) : null;
        }
        public void Insert(RepairBindingModel model)
        {
            using var context = new RepairDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Repair repair = new Repair()
                {
                    RepairName = model.RepairName,
                    Price = model.Price
                }; 
                context.Repairs.Add(repair);
                context.SaveChanges();
                CreateModel(model, repair, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(RepairBindingModel model)
        {
            using var context = new RepairDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Repairs.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(RepairBindingModel model)
        {
            using var context = new RepairDatabase();
            Repair element = context.Repairs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Repairs.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Repair CreateModel(RepairBindingModel model, Repair repair, RepairDatabase context)
        {
            repair.RepairName = model.RepairName;
            repair.Price = model.Price;
            if (model.Id.HasValue)
            {
                var repairComponents = context.RepairComponents.Where(rec => rec.RepairId == model.Id.Value).ToList();
                context.RepairComponents.RemoveRange(repairComponents.Where(rec => !model.RepairComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                foreach (var updateComponent in repairComponents)
                {
                    updateComponent.Count = model.RepairComponents[updateComponent.ComponentId].Item2;
                    model.RepairComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.RepairComponents)
            {
                context.RepairComponents.Add(new RepairComponent
                {
                    RepairId = repair.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return repair;
        }
        private static RepairViewModel CreateModel(Repair repair)
        {
            return new RepairViewModel
            {
                Id = repair.Id,
                RepairName = repair.RepairName,
                Price = repair.Price,
                RepairComponents = repair.RepairComponents.
               ToDictionary(recPC => recPC.ComponentId,
               recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
    }
}
