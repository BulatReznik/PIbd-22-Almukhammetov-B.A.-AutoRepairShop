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
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> WareHouseComponents { get; set; }

        public string GetStringComponents() ////////ДОБАВИЛ
        {
            string stringComponents = string.Empty;
            foreach (var ingr in WareHouseComponents)
            {
                stringComponents += ingr.Key + ") " + ingr.Value.Item1 + ": " + ingr.Value.Item2 + ", ";
            }
            if (stringComponents.Length != 0)
                return stringComponents[0..^2];
            else
                return stringComponents;
        }

    }
}
