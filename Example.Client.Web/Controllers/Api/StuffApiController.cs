using Core.Common.Dtos;
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
    public class StuffApiController : BaseApiController<Stuff>
    {
        public StuffApiController(IClientProxy proxy)
            : base("stuff", proxy)
        {
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> List()
        {
            return await BaseList();
        }

        [HttpPost]
        [Route("search")]
        public async Task<JsonResult> Search(string search, int page = 1, int pageSize = 12)
        {
            return await BaseSearch(search, page, pageSize);
        }

        [HttpPost]
        [Route("reverse-search")]
        public async Task<JsonResult> ReverseSearch(string search, int page = 1, int pageSize = 12)
        {
            var result = await _proxy.Get<PagedResults<Stuff>>($"/{_apiPath}/get-reverse?search={search}&page={page}&pagesize={pageSize}");
            var apiResponse = new ApiResponse<PagedResults<Stuff>>(result);
            return Json(apiResponse);

        }

        [HttpPost]
        [Route("create")]
        public async Task<JsonResult> Create(Stuff newItem)
        {
            return await BaseCreate(newItem);
        }

        [HttpPost]
        [Route("update")]
        public async Task<JsonResult> Update(Stuff updatedItem)
        {
            return await BaseUpdate(updatedItem);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<JsonResult> Delete(Stuff deletedItem)
        {
            return await BaseDelete(deletedItem);
        }
    }
}