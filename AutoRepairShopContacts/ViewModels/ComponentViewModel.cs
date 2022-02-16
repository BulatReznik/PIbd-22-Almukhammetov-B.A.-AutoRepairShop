using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RepairContracts.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class ComponentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название Расходника")]
        public string ComponentName { get; set; }
    }
}
