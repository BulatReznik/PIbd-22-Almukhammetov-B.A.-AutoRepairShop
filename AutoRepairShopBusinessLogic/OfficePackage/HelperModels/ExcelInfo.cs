using System;
using System.Collections.Generic;
using System.Text;
using RepairContracts.ViewModels;

namespace RepairBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportRepairComponentViewModel> RepairComponents { get; set; }
    }
}
