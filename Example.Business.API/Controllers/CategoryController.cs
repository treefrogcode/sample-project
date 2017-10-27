using Core.Common.Dtos;
using Example.Business.API.Attributes;
using Example.Business.Logic.Interfaces;
using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Web.Http;

namespace Example.Business.API.Controllers
{
    [TokenAuthentication(Public = true)]
    [RoutePrefix("category")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryRepository categoryRepository, ICategoryManager categoryManager)
        {
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
        }

        [HttpGet]
        [Route("get")]
        public PagedResults<Category> Get(string search = "", int page = 1, int pageSize = 10)
        {
            var category = _categoryManager.GetCategories(search, page, pageSize);
            return category;
        }

        [HttpPost]
        [Route("create")]
        public Category Create(Category newCategory)
        {
            var category = _categoryRepository.Add(newCategory);
            return category;
        }

        [HttpPut]
        [Route("update")]
        public Category Update(Category updatedCategory)
        {
            var category = _categoryRepository.Update(updatedCategory);
            return category;
        }

        [HttpDelete]
        [Route("delete/{deletedCategoryId}")]
        public bool Delete(int deletedCategoryId)
        {
            return _categoryRepository.Remove(deletedCategoryId);
        }
    }
}
