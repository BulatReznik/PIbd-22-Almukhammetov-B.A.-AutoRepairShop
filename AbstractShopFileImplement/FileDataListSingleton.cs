using RepairContracts.Enums;
using RepairFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RepairFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string RepairFileName = "Repair.xml";
        private readonly string WareHouseFileName = "WareHouse.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Repair> Repairs { get; set; }
        public List<WareHouse> WareHouses { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Repairs = LoadRepairs();
            WareHouses = LoadWareHouses();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("ID").Value),
                        RepairId = Convert.ToInt32(elem.Element("RepairID").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Repair> LoadRepairs()
        {
            var list = new List<Repair>();
            if (File.Exists(RepairFileName))
            {
                var xDocument = XDocument.Load(RepairFileName);
                var xElements = xDocument.Root.Elements("Repair").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("RepairComponents").Elements("RepairComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Repair
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        RepairName = elem.Element("RepairName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        RepairComponents = prodComp
                    });
                }
            }
            return list;
        }
        private List<WareHouse> LoadWareHouses()
        {
            var list = new List<WareHouse>();
            if (File.Exists(WareHouseFileName))
            {
                XDocument xDocument = XDocument.Load(WareHouseFileName);

                var xElements = xDocument.Root.Elements("WareHouse").ToList();

                foreach (var elem in xElements)
                {
                    var wareHouseComponents = new Dictionary<int, int>();
                    foreach (var ingredient in elem.Element("WareHouseComponents").Elements("WareHouseComponent").ToList())
                    {
                        wareHouseComponents.Add(Convert.ToInt32(ingredient.Element("Key").Value), Convert.ToInt32(ingredient.Element("Value").Value));
                    }

                    list.Add(new WareHouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WareHouseName = elem.Element("WareHouseName").Value,
                        ResponsibleName = elem.Element("ResponsibleName").Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        WareHouseComponents = wareHouseComponents
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(
                        new XElement("Order",
                        new XAttribute("ID", order.Id),
                        new XElement("RepairID", order.RepairId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", order.Status),
                        new XElement("DateCreate", order.DateCreate),
                        new XElement("DateImplement", order.DateImplement)
                        ));
                    XDocument xDocument = new(xElement);
                    xDocument.Save(OrderFileName);
                }
                    
            }
        }
        private void SaveRepairs()
        {
            if (Repairs != null)
            {
                var xElement = new XElement("Repairs");
                foreach (var repair in Repairs)
                {
                    var compElement = new XElement("RepairComponents");
                    foreach (var component in repair.RepairComponents)
                    {
                        compElement.Add(new XElement("RepairComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Repair",
                     new XAttribute("Id", repair.Id),
                     new XElement("RepairName", repair.RepairName),
                     new XElement("Price", repair.Price),
                     compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(RepairFileName);
            }
        }
        private void SaveWareHouses()
        {
            if (WareHouses != null)
            {
                var xElement = new XElement("WareHouses");

                foreach (var wareHouse in WareHouses)
                {
                    var compElement = new XElement("WareHouseComponents");

                    foreach (var component in wareHouse.WareHouseComponents)
                    {
                        compElement.Add(new XElement("WareHouseComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }

                    xElement.Add(new XElement("WareHouse",
                        new XAttribute("Id", wareHouse.Id),
                        new XElement("WareHouseName", wareHouse.WareHouseName),
                        new XElement("ResponsibleName", wareHouse.ResponsibleName),
                        new XElement("DateCreate", wareHouse.DateCreate),
                        compElement));
                }

                XDocument xDocument = new(xElement);
                xDocument.Save(WareHouseFileName);
            }
        }
        public static void SaveMethods() 
        {  
            instance.SaveComponents();
            instance.SaveOrders();
            instance.SaveRepairs();
            instance.SaveWareHouses();
        }
    }
}
