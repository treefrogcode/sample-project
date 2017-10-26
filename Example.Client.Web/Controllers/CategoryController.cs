using Core.Common.Dtos;
using Example.Client.Models.Entities;
using Example.Client.Proxy.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Client.Web.Controllers
{
    [RoutePrefix("categories")]
    public class CategoryController : Controller
    {
        private readonly IClientProxy _proxy;

        public CategoryController(IClientProxy proxy)
        {
            _proxy = proxy;
        }

        [Route("")]
        public async Task<ActionResult> Index()
        {
            var categories = await _proxy.Get<PagedResults<Category>>("/category/get");
            return View("index", null, JsonConvert.SerializeObject(categories));
        }
    }
}