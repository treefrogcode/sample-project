using Core.Common.Dtos;
using Example.Business.API.Attributes;
using Example.Business.Logic.Interfaces;
using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Linq;
using System.Web.Http;

namespace Example.Business.API.Controllers
{
    [TokenAuthentication]
    [RoutePrefix("stuff")]
    public class StuffController : BaseController
    {
        private readonly IStuffRepository _stuffRepository;
        private readonly IStuffManager _stuffManager;

        public StuffController(IStuffRepository stuffRepository, IStuffManager stuffManager)
        {
            _stuffRepository = stuffRepository;
            _stuffManager = stuffManager;
        }

        [TokenAuthentication(Public = true)]
        [HttpGet]
        [Route("get")]
        public PagedResults<Stuff> Get(string search = "", int page = 1, int pageSize = 0)
        {
            var stuff = _stuffManager.GetStuff(search, page, pageSize);
            return stuff;
        }

        [HttpGet]
        [Route("get-reverse")]
        public PagedResults<Stuff> GetReversedStuff(string search = "", int page = 1, int pageSize = 0)
        {
            var stuff = _stuffManager.GetReversedStuff(search, page, pageSize);
            return stuff;
        }

        [HttpPost]
        [Route("create")]
        public Stuff Create(Stuff newStuff)
        {
            var stuff = _stuffRepository.Add(newStuff);
            return stuff;
        }

        [HttpPut]
        [Route("update")]
        public Stuff Update(Stuff updatedStuff)
        {
            var stuff = _stuffRepository.Update(updatedStuff);
            return stuff;
        }

        [HttpDelete]
        [Route("delete/{deletedStuffId}")]
        public void Delete(int deletedStuffId)
        {
            _stuffRepository.Remove(deletedStuffId);
        }
    }
}
