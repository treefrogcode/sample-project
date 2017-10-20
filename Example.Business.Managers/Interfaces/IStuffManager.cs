using Example.Business.Models.Entities;
using System.Collections.Generic;

namespace Example.Business.Logic.Interfaces
{
    public interface IStuffManager
    {
        IEnumerable<Stuff> GetReversedStuff();
    }
}
