using Core.Common.Dtos;
using Example.Client.Models.Interfaces;
using Example.Client.Modles.Dtos;
using Example.Client.Proxy.Interfaces;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Client.Web.Controllers.Api
{
    public abstract class BaseApiController<T> : Controller
        where T : class, IIdentifiableEntity, new()
    {
        protected readonly IClientProxy _proxy;
        protected readonly string _apiPath;

        public BaseApiController(string apiPath, IClientProxy proxy)
        {
            _proxy = proxy;
            _apiPath = apiPath;
        }

        public async Task<JsonResult> BaseList()
        {
            var result = await _proxy.Get<PagedResults<T>>($"/{_apiPath}/get?pagesize=0");
            var apiResponse = new ApiResponse<PagedResults<T>>(result);
            return Json(apiResponse);
        }

        public async Task<JsonResult> BaseSearch(string search, int page = 1, int pageSize = 12)
        {
            var result = await _proxy.Get<PagedResults<T>>($"/{_apiPath}/get?search={search}&page={page}&pagesize={pageSize}");
            var apiResponse = new ApiResponse<PagedResults<T>>(result);
            return Json(apiResponse);
        }

        public async Task<JsonResult> BaseCreate(T newItem)
        {
            if (ModelState.IsValid)
            {
                var result = await _proxy.Post($"/{_apiPath}/create", newItem);
                var apiResponse = new ApiResponse<T>(result);
                return Json(apiResponse);
            }
            else
            {
                var apiResponse = new ApiResponse<T>(HttpStatusCode.BadRequest, "Invalid");
                return Json(apiResponse);
            }
        }

        public async Task<JsonResult> BaseUpdate(T updatedItem)
        {
            if (ModelState.IsValid)
            {
                var result = await _proxy.Put($"/{_apiPath}/update", updatedItem);
                var apiResponse = new ApiResponse<T>(result);
                return Json(apiResponse);
            }
            else
            {
                var apiResponse = new ApiResponse<T>(HttpStatusCode.BadRequest, "Invalid");
                return Json(apiResponse);
            }
        }

        public async Task<JsonResult> BaseDelete(T deletedItem)
        {
            await _proxy.Delete($"/{_apiPath}/delete/{deletedItem.EntityId}");
            var apiResponse = new ApiResponse<T>(deletedItem);
            return Json(apiResponse);
        }
    }
}