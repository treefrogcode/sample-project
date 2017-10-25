using Core.Common.Dtos;
using Example.Client.Models.Entities;
using Example.Client.Proxy.Interfaces;
using Example.Client.Web.ViewModels;
using Newtonsoft.Json;
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

        [Route("reverse")]
        public async Task<ActionResult> GetReverse()
        {
            var response = await _proxy.Get<PagedResults<Stuff>>("/stuff/get-reverse");
            return View("index", response.Results);
        }

        [Route("")]
        [Route("react")]
        [Route("stuff")]
        public async Task<ActionResult> Stuff()
        {
            var pagedResults = await _proxy.Get<PagedResults<Stuff>>("/stuff/get?pagesize=12");
            var colours = await _proxy.Get<PagedResults<Colour>>("/colour/get");
            var categories = await _proxy.Get<PagedResults<Category>>("/category/get");

            var model = new StuffViewModel();
            model.PagedResults = pagedResults;
            model.Colours = colours.Results;
            model.Categories = categories.Results;

            return View("stuff", null, JsonConvert.SerializeObject(model));
        }

        [Route("colour")]
        public async Task<ActionResult> Colour()
        {
            var response = await _proxy.GetAsJson("/colour/get");
            return View("colour", null, response);
        }
    }
}