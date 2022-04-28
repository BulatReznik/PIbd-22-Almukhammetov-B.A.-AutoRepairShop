using System;
using System.Collections.Generic;
using System.Text;
using RepairBusinessLogic.OfficePackage.HelperEnums;
using RepairContracts.ViewModels;

namespace RepairBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public WordDocumentType DocumentType { get; set; }
        public List<RepairViewModel> Repairs { get; set; }
        public List<WareHouseViewModel> WareHouses { get; set; }
    }
}
