using Core.Common.Dtos;
using Example.Business.Models.Entities;

namespace Example.Business.Logic.Interfaces
{
    public interface IColourManager
    {
        PagedResults<Colour> GetColours(string search = "", int page = 1, int pageSize = 10);
    }
}
