using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using RepairDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairDatabaseImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        public List<ImplementerViewModel> GetFullList()
        {
            using var context = new RepairDatabase();
            return context.Implementers.Select(CreateModel).ToList();
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            return context.Implementers.Where(rec => rec.ImplementerFIO == model.ImplementerFIO).Select(CreateModel).ToList();
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            var implementer = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id || rec.ImplementerFIO == model.ImplementerFIO);
            return implementer != null ? CreateModel(implementer) : null;
        }

        public void Insert(ImplementerBindingModel model)
        {
            using var context = new RepairDatabase();
            context.Implementers.Add(CreateModel(model, new Implementer()));
            context.SaveChanges();
        }

        public void Update(ImplementerBindingModel model)
        {
            using var context = new RepairDatabase();
            var element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Исполнитель не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(ImplementerBindingModel model)
        {
            using var context = new RepairDatabase();
            Implementer implementer = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (implementer != null)
            {
                context.Implementers.Remove(implementer);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Исполнитель не найден");
            }
        }

        private static Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            implementer.ImplementerFIO = model.ImplementerFIO;
            implementer.WorkingTime = model.WorkingTime;
            implementer.PauseTime = model.PauseTime;
            return implementer;
        }
        private ImplementerViewModel CreateModel(Implementer implementer)
        {
            return new ImplementerViewModel
            {
                Id = implementer.Id,
                ImplementerFIO = implementer.ImplementerFIO,
                WorkingTime = implementer.WorkingTime,
                PauseTime = implementer.PauseTime
            };
        }
    }
}
