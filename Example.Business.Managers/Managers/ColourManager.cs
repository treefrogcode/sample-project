using Example.Business.Logic.Interfaces;
using Example.Business.Logic.Utils;
using Core.Common.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Example.Business.Logic.Managers
{
    public class ColourManager : IColourManager
    {
        private readonly IColourRepository _colourRepository;

        public ColourManager(IColourRepository colourRepository)
        {
            _colourRepository = colourRepository;
        }

        public PagedResults<Colour> GetColours(string search = "", int page = 1, int pageSize = 4)
        {
            var colours = _colourRepository.Get();

            colours = SearchColours(colours, search);

            return CollectionUtils.PageResults(colours, page, pageSize);
        }

        private IEnumerable<Colour> SearchColours(IEnumerable<Colour> colours, string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                colours = colours.Where(s => s.Name.ToLower().IndexOf(search) >= 0);
            }

            return colours;
        }
    }
}