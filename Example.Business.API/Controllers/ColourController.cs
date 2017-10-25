﻿using Example.Business.API.Attributes;
using Example.Business.Logic.Managers;
using Example.Business.Logic.Interfaces;
using Core.Common.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace Example.Business.API.Controllers
{
    [TokenAuthentication]
    [RoutePrefix("colour")]
    public class ColourController : BaseController
    {
        private readonly IColourRepository _colourRepository;
        private readonly IColourManager _colourManager;

        public ColourController(IColourRepository colourRepository, IColourManager colourManager)
        {
            _colourRepository = colourRepository;
            _colourManager = colourManager;
        }

        [HttpGet]
        [Route("get")]
        public PagedResults<Colour> Get(string search = "", int page = 1, int pageSize = 0)
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
        public void Delete(int deletedColourId)
        {
            _colourRepository.Remove(deletedColourId);
        }
    }
}