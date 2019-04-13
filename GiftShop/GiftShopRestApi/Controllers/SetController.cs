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
    public class SetController : ApiController
    {
        private readonly ISetService _service;

        public SetController(ISetService service)
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

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(SetBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(SetBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(SetBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
