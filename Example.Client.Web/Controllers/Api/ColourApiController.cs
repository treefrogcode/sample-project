using Core.Common.Dtos;
using Example.Client.Models.Entities;
using Example.Client.Modles.Dtos;
using Example.Client.Proxy.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Client.Web.Controllers.Api
{
    [RoutePrefix("api/colour")]
    public class ColourApiController : BaseApiController<Colour>
    {
        public ColourApiController(IClientProxy proxy)
            : base("colour", proxy)
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
        public async Task<JsonResult> Create(Colour newItem)
        {
            return await BaseCreate(newItem);
        }

        [HttpPost]
        [Route("update")]
        public async Task<JsonResult> Update(Colour updatedItem)
        {
            return await BaseUpdate(updatedItem);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<JsonResult> Delete(Colour deletedItem)
        {
            return await BaseDelete(deletedItem);
        }
    }
}