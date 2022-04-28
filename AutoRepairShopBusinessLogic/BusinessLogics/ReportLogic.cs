using RepairBusinessLogic.OfficePackage;
using RepairBusinessLogic.OfficePackage.HelperModels;
using RepairBusinessLogic.OfficePackage.HelperEnums;
using RepairContracts.BindingModels;
using RepairContracts.BusinessLogicsContracts;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IRepairStorage _repairStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IWareHouseStorage _wareHouseStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(IRepairStorage repairStorage, IComponentStorage componentStorage, IOrderStorage orderStorage, IWareHouseStorage wareHouseStorage,
        AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _repairStorage = repairStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _wareHouseStorage = wareHouseStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportRepairComponentViewModel> GetRepairComponent()
        {
            var components = _componentStorage.GetFullList();
            var repairs = _repairStorage.GetFullList();
            var list = new List<ReportRepairComponentViewModel>();
            foreach (var repair in repairs)
            {
                var record = new ReportRepairComponentViewModel
                {
                    RepairName = repair.RepairName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (repair.RepairComponents.ContainsKey(component.Id))
                    {
                        record.Components.Add(new Tuple<string, int>(component.ComponentName, repair.RepairComponents[component.Id].Item2));
                        record.TotalCount += repair.RepairComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка компонент с указанием, на каких складах они находятся
        /// </summary>
        /// <returns></returns>
        public List<ReportWareHouseComponentViewModel> GetWareHouseComponent()
        {
            var wareHouses = _wareHouseStorage.GetFullList();
            var list = new List<ReportWareHouseComponentViewModel>();
            foreach (var wh in wareHouses)
            {
                var record = new ReportWareHouseComponentViewModel
                {
                    WareHouseName = wh.WareHouseName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var ingr in wh.WareHouseComponents)
                {
                    record.Components.Add(new Tuple<string, int>(ingr.Value.Item1, ingr.Value.Item2));
                    record.TotalCount += ingr.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                RepairName = x.RepairName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        public List<ReportOrdersInfoViewModel> GetOrdersGroupByDate()
        {
            return _orderStorage.GetFullList().GroupBy(x => x.DateCreate.Date)
            .Select(x => new ReportOrdersInfoViewModel
            {
                DateCreate = x.Key,
                Count = x.Count(),
                Sum = x.Sum(rec => rec.Sum),
            }).ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список ремонтов",
                Repairs = _repairStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveRepairComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                RepairComponents = GetRepairComponent(),
                SheetType = ExcelSheetType.WareHouse
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model),
                Type = PdfReportType.Filtered
            });
        }
        public void SaveOrdersInfoToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Информация о заказах",
                OrdersInfo = GetOrdersGroupByDate(),
                Type = PdfReportType.All
            });
        }
        public void SaveWareHousesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Таблица складов",
                WareHouses = _wareHouseStorage.GetFullList(),
                DocumentType = WordDocumentType.Table
            });
        }

        public void SaveWareHouseComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                WareHouseComponents = GetWareHouseComponent(),
                SheetType = ExcelSheetType.WareHouse
            });
        }
    }
}
