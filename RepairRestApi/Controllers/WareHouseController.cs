using Microsoft.AspNetCore.Mvc;
using RepairContracts.BindingModels;
using RepairContracts.BusinessLogicsContracts;
using RepairContracts.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RepairRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WareHouseController : Controller
    {
        private readonly IWareHouseLogic _wareHouseLogic;
        private readonly IComponentLogic _componentLogic;
        public WareHouseController(IWareHouseLogic wareHouselogic, IComponentLogic componentLogic)
        {
            _wareHouseLogic = wareHouselogic;
            _componentLogic = componentLogic;
        }
        [HttpGet]
        public List<WareHouseViewModel> GetWareHouses() => _wareHouseLogic.Read(null)?.ToList();
        [HttpGet]
        public WareHouseViewModel GetWareHouse(int wareHouseId) => _wareHouseLogic.Read(new WareHouseBindingModel { Id = wareHouseId })?[0];
        [HttpGet]
        public List<ComponentViewModel> GetComponents() => _componentLogic.Read(null)?.ToList();
        [HttpPost]
        public void CreateUpdateWareHouse(WareHouseBindingModel model) => _wareHouseLogic.CreateOrUpdate(model);
        [HttpPost]
        public void DeleteWareHouse(WareHouseBindingModel model) => _wareHouseLogic.Delete(model);
        [HttpPost]
        public void ReplenishWareHouse(ReplenishBindingModel model) =>
           _wareHouseLogic.AddComponent(new WareHouseBindingModel { Id = model.WareHouseId }, model.ComponentId, model.Count);
    }
}
