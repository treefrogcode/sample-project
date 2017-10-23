using Example.Business.Logic.Interfaces;
using Example.Business.Logic.Utils;
using Core.Common.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Example.Business.Logic.Managers
{
    public class StuffManager : IStuffManager
    {
        private readonly IStuffRepository _stuffRepository;

        public StuffManager(IStuffRepository stuffRepositoy)
        {
            _stuffRepository = stuffRepositoy;
        }

        public PagedResults<Stuff> GetStuff(string search = "", int page = 1, int pageSize = 4)
        {
            var stuff = _stuffRepository.Get();

            stuff = SearchStuff(stuff, search);

            return CollectionUtils.PageResults(stuff, page, pageSize);
        }

        public PagedResults<Stuff> GetReversedStuff(string search = "", int page = 1, int pageSize = 4)
        {
            var stuff = _stuffRepository.Get();

            stuff = SearchStuff(stuff, search).Reverse();

            return CollectionUtils.PageResults(stuff, page, pageSize);
        }

        private IEnumerable<Stuff> SearchStuff(IEnumerable<Stuff> stuff, string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                stuff = stuff.Where(s => s.One.ToLower() == search || s.Two.ToLower() == search || s.Three.ToLower() == search);
            }

            return stuff;
        }
    }
}