using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairBusinessLogic.OfficePackage.HelperEnums;

namespace RepairBusinessLogic.OfficePackage.HelperModels
{
    public class WordTableProperties
    {
        public string TextSize { get; set; }
        public string BorderSize { get; set; }
        public WordBorderType BorderType { get; set; }
    }
}
