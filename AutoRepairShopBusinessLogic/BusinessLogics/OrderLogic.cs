using RepairBusinessLogic.BusinessLogics;
using RepairContracts.BindingModels;
using RepairContracts.BusinessLogicsContracts;
using RepairContracts.Enums;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepairBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IWareHouseStorage _wareHouseStorage;
        public OrderLogic(IOrderStorage orderStorage, IWareHouseStorage wareHouseStorage)
        {
            _orderStorage = orderStorage;
            _wareHouseStorage = wareHouseStorage;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if(model == null) 
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue) 
            {
                return new List<OrderViewModel>
                {
                    _orderStorage.GetElement(model)
                };
            }
            return _orderStorage.GetFilteredList(model);
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                RepairId = model.RepairId,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят,
                DateCreate = DateTime.Now,
                ClientId = model.ClientId
            });
        }

        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel 
            {
                Id = model.OrderId 
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (order.Status != Enum.GetName(typeof(OrderStatus), 2))
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                RepairId = order.RepairId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Выдан,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel 
            {
                Id = model.OrderId 
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if(order.Status == Enum.GetName(typeof(OrderStatus), 4))
            {
                return;
            }
            if (order.Status != Enum.GetName(typeof(OrderStatus), 1))
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                RepairId = order.RepairId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId
            });
        }


        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel 
            { 
                Id = model.OrderId 
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (order.Status != Enum.GetName(typeof(OrderStatus), 0) && !order.Status.Equals(Enum.GetName(typeof(OrderStatus), 4)))
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            if (!_wareHouseStorage.CheckWriteOff(new CheckWriteOffBindingModel 
            {
                RepairId = order.RepairId,
                Count = order.Count
            }
            ))
            {
                order.Status = Enum.GetName(OrderStatus.Требуются_материалы);
            }
            else order.Status = Enum.GetName(OrderStatus.Выполняется);

            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                RepairId = order.RepairId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = Enum.Parse<OrderStatus>(order.Status),
                ClientId = order.ClientId,
                ImplementerId = model.ImplementerId,
            });
        }
    }
}
