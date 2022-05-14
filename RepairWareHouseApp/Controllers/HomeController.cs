using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using RepairWareHouseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RepairWareHouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (Program.IsAuthorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<WareHouseViewModel>>($"api/wareHouse/getwareHouses"));

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (_configuration["Password"] != password)
                {
                    throw new Exception("Неверный пароль");
                }
                Program.IsAuthorized = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите пароль");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public void Create(string wareHouseName, string responsibleName)
        {
            if (string.IsNullOrEmpty(wareHouseName) || string.IsNullOrEmpty(responsibleName))
            {
                return;
            }
            APIClient.PostRequest("api/wareHouse/createupdatewareHouse", new WareHouseBindingModel
            {
                WareHouseName = wareHouseName,
                ResponsibleName = responsibleName,
                DateCreate = DateTime.Now,
                WareHouseComponents = new Dictionary<int, (string, int)>()
            });
            Response.Redirect("Index");
        }
        [HttpGet]
        public IActionResult Change(int wareHouseId)
        {
            ViewBag.WareHouse =
            APIClient.GetRequest<WareHouseViewModel>($"api/wareHouse/getwareHouse?wareHouseId={wareHouseId}");
            return View();
        }
        [HttpPost]
        public void Change(int wareHouseId, string wareHouseName, string responsibleName)
        {
            if (wareHouseId == 0)
            {
                return;
            }
            WareHouseViewModel wareHouse = APIClient.GetRequest<WareHouseViewModel>($"api/Warehouse/getwareHouse?wareHouseId={wareHouseId}");
            APIClient.PostRequest("api/wareHouse/createupdatewareHouse", new WareHouseBindingModel
            {
                Id = wareHouseId,
                WareHouseName = wareHouseName,
                ResponsibleName = responsibleName,
                WareHouseComponents = wareHouse.WareHouseComponents,
                DateCreate = wareHouse.DateCreate
            });
            Response.Redirect("Index");
        }
        [HttpGet]
        public IActionResult Delete(int wareHouseId)
        {
            ViewBag.WareHouse = APIClient.GetRequest<WareHouseViewModel>($"api/wareHouse/getwareHouse?wareHouseId={wareHouseId}");

            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public void DeleteConfirmed(int wareHouseId)
        {
            if (wareHouseId == 0)
            {
                return;
            }
            APIClient.PostRequest("api/wareHouse/deletewareHouse", new WareHouseBindingModel
            {
                Id = wareHouseId
            });
            Response.Redirect("Index");
        }
        [HttpGet]
        public IActionResult Replenish(int wareHouseId)
        {
            ViewBag.WareHouse = APIClient.GetRequest<WareHouseViewModel>($"api/wareHouse/getwareHouse?wareHouseId={wareHouseId}");
            ViewBag.Components = APIClient.GetRequest<List<ComponentViewModel>>($"api/wareHouse/getcomponents");
            return View();
        }
        [HttpPost]
        public void Replenish(int wareHouseId, int componentId, int count)
        {
            if (wareHouseId == 0 || componentId == 0 || count < 0)
            {
                return;
            }
            APIClient.PostRequest("api/wareHouse/replenishwareHouse", new ReplenishBindingModel
            {
                WareHouseId = wareHouseId,
                ComponentId = componentId,
                Count = count,
            });
            Response.Redirect("Index");
        }
    }
}
