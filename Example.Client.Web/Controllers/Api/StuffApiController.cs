using Example.Client.Models.Entities;
using Example.Client.Proxy.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Client.Web.Controllers.Api
{
    [RoutePrefix("api/stuff")]
    public class StuffApiController : BaseApiController
    {
        private readonly IClientProxy _proxy;
        public StuffApiController(IClientProxy proxy)
        {
            _proxy = proxy;
        }

        [HttpPost]
        [Route("search")]
        public async Task<JsonResult> Search(string search)
        {
            var result = await _proxy.Get<List<Stuff>>($"/stuff/get?search={search}");
            return Json(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<JsonResult> CreateStuff(Stuff newStuff)
        {
            var result = await _proxy.Post("/stuff/create", newStuff);
            return Json(result);
        }

        [HttpPost]
        [Route("update")]
        public async Task<JsonResult> UpdateStuff(Stuff updatedStuff)
        {
            var result = await _proxy.Put("/stuff/update", updatedStuff);
            return Json(result);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<JsonResult> DeleteStuff(Stuff deletedStuff)
        {
            await _proxy.Delete($"/stuff/delete/{deletedStuff.StuffId}");
            return Json(deletedStuff);
        }
    }
}