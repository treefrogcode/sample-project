using Example.Business.Logic.Managers;
using Example.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using Example.Business.Models.Entities;

namespace Example.Business.Logic.Tests.Managers
{
    public class CategoryManagerTests
    {
        private readonly CategoryManager _categoryManager;
        private readonly Mock<ICategoryRepository> _categoryRepository;

        private readonly List<Category> _categories;

        public CategoryManagerTests()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _categoryManager = new CategoryManager(_categoryRepository.Object);

            _categories = new List<Category>();
            _categories.Add(new Category { CategoryId = 1, Name = "Cat One" });
            _categories.Add(new Category { CategoryId = 2, Name = "Cat Two" });
            _categories.Add(new Category { CategoryId = 3, Name = "Cat Three" });
            _categories.Add(new Category { CategoryId = 4, Name = "Cat Four" });
            _categories.Add(new Category { CategoryId = 5, Name = "Cat Five" });
            _categories.Add(new Category { CategoryId = 6, Name = "Cat Six" });
        }

        [Fact]
        public void GetCategories_GetAll()
        {
            // Arrange
            var expected = 6;
            _categoryRepository.Setup(r => r.Get()).Returns(_categories);

            // Act
            var results = _categoryManager.GetCategories(string.Empty, 1, 10);

            // Assert
            Assert.Equal(expected, results.Results.Count());
            Assert.Equal(expected, results.Paging.TotalRecords);
        }

        [Fact]
        public void GetCategories_GetOneBySearch()
        {
            // Arrange
            var expected = 1;
            var search = "five";
            _categoryRepository.Setup(r => r.Get()).Returns(_categories);

            // Act
            var results = _categoryManager.GetCategories(search, 1, 10);

            // Assert
            Assert.Equal(expected, results.Results.Count());
            Assert.Contains(search, results.Results.First().Name.ToLower());
            Assert.Equal(expected, results.Paging.TotalRecords);
        }
    }
}