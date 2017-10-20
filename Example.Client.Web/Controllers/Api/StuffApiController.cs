using Example.Client.Models.Entities;
using Example.Client.Modles.Dtos;
using Example.Client.Proxy.Interfaces;
using System.Collections.Generic;
using System.Net;
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
            var apiResponse = new ApiResponse<List<Stuff>>(result);
            return Json(apiResponse);
        }

        [HttpPost]
        [Route("create")]
        public async Task<JsonResult> CreateStuff(Stuff newStuff)
        {
            if (ModelState.IsValid)
            {
                var result = await _proxy.Post("/stuff/create", newStuff);
                var apiResponse = new ApiResponse<Stuff>(result);
                return Json(apiResponse);
            }
            else
            {
                var apiResponse = new ApiResponse<Stuff>(HttpStatusCode.BadRequest, "Invalid data");
                return Json(apiResponse);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<JsonResult> UpdateStuff(Stuff updatedStuff)
        {
            if (ModelState.IsValid)
            {
                var result = await _proxy.Put("/stuff/update", updatedStuff);
                var apiResponse = new ApiResponse<Stuff>(result);
                return Json(apiResponse);
            }
            else
            {
                var apiResponse = new ApiResponse<Stuff>(HttpStatusCode.BadRequest, "Invalid data");
                return Json(apiResponse);
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<JsonResult> DeleteStuff(Stuff deletedStuff)
        {
            await _proxy.Delete($"/stuff/delete/{deletedStuff.StuffId}");
            var apiResponse = new ApiResponse<Stuff>(deletedStuff);
            return Json(apiResponse);
        }
    }
}