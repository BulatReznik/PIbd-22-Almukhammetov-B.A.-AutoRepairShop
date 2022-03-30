﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace RepairContracts.BindingModels
{
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>
    [DataContract]
    public class CreateOrderBindingModel
    {
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int RepairId { get; set; }
        [DataMember]
        public int Count  { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
