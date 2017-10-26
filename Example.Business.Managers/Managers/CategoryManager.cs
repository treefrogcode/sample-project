using Example.Business.Logic.Interfaces;
using Example.Business.Logic.Utils;
using Core.Common.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Example.Business.Logic.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public PagedResults<Category> GetCategories(string search = "", int page = 1, int pageSize = 4)
        {
            var categorys = _categoryRepository.Get();

            categorys = SearchCategorys(categorys, search);

            return CollectionUtils.PageResults(categorys, page, pageSize);
        }

        private IEnumerable<Category> SearchCategorys(IEnumerable<Category> categories, string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                categories = categories.Where(s => s.Name.ToLower().IndexOf(search) >= 0);
            }

            return categories;
        }
    }
}