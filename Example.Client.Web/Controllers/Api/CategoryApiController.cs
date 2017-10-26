using Core.Common.Dtos;
using Example.Client.Models.Entities;
using Example.Client.Modles.Dtos;
using Example.Client.Proxy.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Client.Web.Controllers.Api
{
    [RoutePrefix("api/category")]
    public class CategoryApiController : BaseApiController<Category>
    {
        public CategoryApiController(IClientProxy proxy)
            : base("category", proxy)
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
        public async Task<JsonResult> Search(string search, int page = 1, int pageSize = 10)
        {
            return await BaseSearch(search, page, pageSize);
        }

        [HttpPost]
        [Route("create")]
        public async Task<JsonResult> Create(Category newItem)
        {
            return await BaseCreate(newItem);
        }

        [HttpPost]
        [Route("update")]
        public async Task<JsonResult> Update(Category updatedItem)
        {
            return await BaseUpdate(updatedItem);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<JsonResult> Delete(Category deletedItem)
        {
            return await BaseDelete(deletedItem);
        }
    }
}