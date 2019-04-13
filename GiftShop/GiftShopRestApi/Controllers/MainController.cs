using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;

namespace GiftShopRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;

        public MainController(IMainService service)
        {
            _service = service;
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
        public void TakeProcedureInWork(ProcedureBindingModel model)
        {
            _service.TakeProcedureInWork(model);
        }

        [HttpPost]
        public void FinishProcedure(ProcedureBindingModel model)
        {
            _service.FinishProcedure(model);
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
    }
}
