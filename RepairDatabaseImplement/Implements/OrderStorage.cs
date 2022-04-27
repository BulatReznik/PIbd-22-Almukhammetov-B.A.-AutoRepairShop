using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using RepairContracts.StorageContracts;
using RepairDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new RepairDatabase();
            return context.Orders.Include(rec => rec.Repair).Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                RepairId = rec.RepairId,
                RepairName = rec.Repair.RepairName,
                Count = rec.Count,
                Sum = rec.Sum,
                Status = rec.Status.ToString(),
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement,
                ClientId = rec.ClientId.Value,
                ClientFIO = rec.Client.ClientFIO,
                ImplementerId = rec.ImplementerId.Value,
                ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty
            })
            .ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            return context.Orders.Include(rec => rec.Repair)
                .Include(rec => rec.Client)
                .Include(rec => rec.Implementer)
                .Where(rec => (rec.RepairId == model.RepairId) ||
               (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo) ||
               (model.ClientId.HasValue && rec.ClientId == model.ClientId)||
               (model.SearchStatus.HasValue && model.SearchStatus.Value == rec.Status)||
               (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && model.Status == rec.Status))
               .Select(rec => new OrderViewModel
               {
                    Id = rec.Id,
                    RepairId = rec.RepairId,
                    RepairName = rec.Repair.RepairName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status.ToString(),
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    ClientId = rec.ClientId.Value,
                    ClientFIO = rec.Client.ClientFIO,
                    ImplementerId = rec.ImplementerId.Value,
                    ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty
               })
               .ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            var order = context.Orders
                .Include(rec => rec.Repair)
                .Include(rec => rec.Client)
                .Include(rec => rec.Implementer)
                .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order, context) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using var context = new RepairDatabase();
            context.Orders.Add(CreateModel(model, new Order()));
            context.SaveChanges();
        }
        public void Update(OrderBindingModel model)
        {
            using var context = new RepairDatabase();
            var element = context.Orders.Include(rec => rec.Repair)
                .Include(rec => rec.Client)
                .Include(rec => rec.Implementer)
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new RepairDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.RepairId = model.RepairId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            order.ClientId = model.ClientId.Value;
            order.ImplementerId = model.ImplementerId;
            return order;
        }
        public OrderViewModel CreateModel(Order order, RepairDatabase context)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                RepairId = order.RepairId,
                RepairName = context.Repairs.FirstOrDefault(rec => rec.Id == order.RepairId)?.RepairName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                ClientId = order.ClientId.Value,
                ClientFIO = order.Client.ClientFIO,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = order.ImplementerId.HasValue ? order.Implementer.ImplementerFIO : string.Empty
            };
        }
    }
}
