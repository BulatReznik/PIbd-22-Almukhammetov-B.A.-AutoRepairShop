﻿using RepairBusinessLogic.OfficePackage.HelperEnums;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace RepairBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportOrdersViewModel> Orders { get; set; }
        public List<ReportOrdersInfoViewModel> OrdersInfo { get; set; }
        public PdfReportType Type { get; set; }
    }
}
