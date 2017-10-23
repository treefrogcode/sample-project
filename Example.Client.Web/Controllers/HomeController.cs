using Core.Common.Dtos;
using Example.Client.Models.Entities;
using Example.Client.Proxy.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Client.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IClientProxy _proxy;
        public HomeController(IClientProxy proxy)
        {
            _proxy = proxy;
        }

        [Route("")]
        [Route("get")]
        public async Task<ActionResult> Get()
        {
            var response = await _proxy.Get<PagedResults<Stuff>>("/stuff/get");
            return View("index", response.Results);
        }

        [Route("reverse")]
        public async Task<ActionResult> GetReverse()
        {
            var response = await _proxy.Get<PagedResults<Stuff>>("/stuff/get-reverse");
            return View("index", response.Results);
        }

        [Route("react")]
        public async Task<ActionResult> React()
        {
            var response = await _proxy.GetAsJson("/stuff/get");
            return View("react", null, response);
        }
    }
}