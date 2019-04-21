using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopRestApi.Services;

namespace GiftShopRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;

        private readonly ISellerService _serviceSeller;

        public MainController(IMainService service, ISellerService
            serviceSeller)
        {
            _service = service;
            _serviceSeller = serviceSeller;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateProcedure(ProcedureBindingModel model)
        {
            _service.CreateProcedure(model);
        }

        [HttpPost]
        public void PayProcedure(ProcedureBindingModel model)
        {
            _service.PayProcedure(model);
        }

        [HttpPost]
        public void PutPartOnStorage(StoragePartBindingModel model)
        {
            _service.PutPartOnStorage(model);
        }
        [HttpPost]
        public void StartWork()
        {
            List<ProcedureViewModel> procedures = _service.GetFreeProcedures();
            foreach (var procedure in procedures)
            {
                SellerViewModel sel = _serviceSeller.GetFreeSeller();
                if (sel == null)
                {
                    throw new Exception("Нет продавцов");
                }
                new WorkSeller(_service, _serviceSeller, sel.Id, procedure.Id);
            }
        }
    }
}
