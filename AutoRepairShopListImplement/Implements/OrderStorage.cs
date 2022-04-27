﻿using System;
using System.Collections.Generic;
using System.Text;
using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using RepairListImplement.Models;

namespace RepairListImplement.Implements
{
    public class OrderStorage: IOrderStorage
    {
        private readonly DataListSingleton source;
        public OrderStorage() 
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList() 
        {
            var result = new List<OrderViewModel>();
            foreach(var order in source.Orders) 
            {
                result.Add(CreateModel(order));
            }
            return result;
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id || order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo )
                {
                    result.Add(CreateModel(order));
                }
            }
            return result;
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id || order.RepairId == model.RepairId)
                {
                    return CreateModel(order);
                }
            }
            return null;
        }

        public void Insert(OrderBindingModel model)
        {
            Order tempOrder = new Order 
            {
                Id = 1 
            };
            foreach (var order in source.Orders)
            {
                if (order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }
        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.RepairId = model.RepairId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;

            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string RepairName = null;
            for (int j = 0; j < source.Repairs.Count; ++j)
            {
                if (source.Repairs[j].Id == order.RepairId)
                {
                    RepairName = source.Repairs[j].RepairName;
                    break;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                RepairId = order.RepairId,
                RepairName = RepairName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
