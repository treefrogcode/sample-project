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

        public IEnumerable<Stuff> GetReversedStuff()
        {
            var stuff = _stuffRepository.Get();
            return stuff.Reverse();
        }
    }
}