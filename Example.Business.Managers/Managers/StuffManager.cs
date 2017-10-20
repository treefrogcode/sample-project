using Example.Business.Logic.Interfaces;
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

        public IEnumerable<Stuff> GetStuff(string search = "")
        {
            var stuff = _stuffRepository.Get();

            stuff = SearchStuff(stuff, search);

            return stuff;
        }

        public IEnumerable<Stuff> GetReversedStuff(string search = "")
        {
            var stuff = _stuffRepository.Get();

            stuff = SearchStuff(stuff, search);

            return stuff.Reverse();
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