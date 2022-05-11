using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using RepairContracts.Attributes;

namespace RepairContracts.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 50, visible: false)]
        public int Id { get; set; }
        [Column(title: "Название ингредиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }
}
