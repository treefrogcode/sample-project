using Core.Common.Dtos;
using Example.Business.API.Attributes;
using Example.Business.Logic.Interfaces;
using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Web.Http;

namespace Example.Business.API.Controllers
{
    [TokenAuthentication(Public = true)]
    [RoutePrefix("colour")]
    public class ColourController : BaseController
    {
        private readonly IStuffRepository _stuffRepository;
        private readonly IColourRepository _colourRepository;
        private readonly IColourManager _colourManager;

        public ColourController(IStuffRepository stuffRepository, IColourRepository colourRepository, IColourManager colourManager)
        {
            _stuffRepository = stuffRepository;
            _colourRepository = colourRepository;
            _colourManager = colourManager;
        }

        [HttpGet]
        [Route("get")]
        public PagedResults<Colour> Get(string search = "", int page = 1, int pageSize = 10)
        {
            var colour = _colourManager.GetColours(search, page, pageSize);
            return colour;
        }

        [HttpGet]
        [Route("get-admin")]
        public PagedResults<Colour> GetAdmin(string search = "", int page = 1, int pageSize = 10)
        {
            var colour = _colourManager.GetColours(search, page, pageSize);
            return colour;
        }

        [HttpPost]
        [Route("create")]
        public Colour Create(Colour newColour)
        {
            var colour = _colourRepository.Add(newColour);
            return colour;
        }

        [HttpPut]
        [Route("update")]
        public Colour Update(Colour updatedColour)
        {
            var colour = _colourRepository.Update(updatedColour);
            return colour;
        }

        [HttpDelete]
        [Route("delete/{deletedColourId}")]
        public bool Delete(int deletedColourId)
        {
            return _colourRepository.Remove(deletedColourId);
        }
    }
}
