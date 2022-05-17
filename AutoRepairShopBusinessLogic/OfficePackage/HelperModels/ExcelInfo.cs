using System;
using System.Collections.Generic;
using System.Text;
using RepairBusinessLogic.OfficePackage.HelperEnums;
using RepairContracts.ViewModels;

namespace RepairBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public ExcelSheetType SheetType { get; set; }
        public List<ReportRepairComponentViewModel> RepairComponents { get; set; }
        public List<ReportWareHouseComponentViewModel> WareHouseComponents { get; set; }
    }
}
