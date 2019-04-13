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
    public class RecordController : ApiController
    {
        private readonly IRecordService _service;

        public RecordController(IRecordService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetStoragesLoad()
        {
            var list = _service.GetStoragesLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetCustomerProcedures(RecordBindingModel model)
        {
            var list = _service.GetCustomerProcedures(model); if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveSetPrice(RecordBindingModel model)
        {
            _service.SaveSetPrice(model);
        }

        [HttpPost]
        public void SaveStocksLoad(RecordBindingModel model)
        {
            _service.SaveStoragesLoad(model);
        }

        [HttpPost]
        public void SaveCustomerProcedures(RecordBindingModel model)
        {
            _service.SaveCustomerProcedures(model);
        }
    }
}
