using Core.Common.Dtos;
using Example.Client.Models.Entities;
using Example.Client.Proxy.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Client.Web.Controllers
{
    [RoutePrefix("colours")]
    public class ColourController : Controller
    {
        private readonly IClientProxy _proxy;

        public ColourController(IClientProxy proxy)
        {
            _proxy = proxy;
        }

        [Route("")]
        public async Task<ActionResult> Index()
        {
            var colours = await _proxy.Get<PagedResults<Colour>>("/colour/get");
            return View("index", null, JsonConvert.SerializeObject(colours));
        }
    }
}