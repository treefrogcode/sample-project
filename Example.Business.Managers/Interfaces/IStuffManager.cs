using Core.Common.Dtos;
using Example.Business.Models.Entities;

namespace Example.Business.Logic.Interfaces
{
    public interface IStuffManager
    {
        PagedResults<Stuff> GetStuff(string search = "", int page = 1, int pageSize = 10);
        PagedResults<Stuff> GetReversedStuff(string search = "", int page = 1, int pageSize = 10);
    }
}
