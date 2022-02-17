using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RepairContracts.ViewModels
{
    public class WareHouseViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string WareHouseName { get; set; }
        [DisplayName("Сотрудник")]
        public string ResponsibleName { get; set; }
        [DisplayName("Дата создания склада")]
        public DateTime DateCreation { get; set; }
        public Dictionary<int, (string, int)> WareHouseComponents { get; set; }
    }
}
