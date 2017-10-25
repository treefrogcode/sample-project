using Core.Common.Dtos;
using Example.Business.Models.Entities;

namespace Example.Business.Logic.Interfaces
{
    public interface ICategoryManager
    {
        PagedResults<Category> GetCategories(string search = "", int page = 1, int pageSize = 0);
    }
}
