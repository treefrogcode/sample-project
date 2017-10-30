using Example.Business.Models.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Context;
using Example.Data.Repositories;
using Example.Data.Tests.Helpers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xunit;

namespace Example.Data.Tests.Repositories
{
    public class CategoryRepositoryTests
    {
        private readonly Mock<ExampleContext> _dbContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<Session> _session;
        private readonly CategoryRepository _repository;

        public CategoryRepositoryTests()
        {
            _dbContext = new Mock<ExampleContext>();
            _httpContext = new Mock<HttpContextBase>();
            _session = new Mock<Session>();
            _repository = new CategoryRepository(_dbContext.Object, _httpContext.Object, _session.Object);
        }

        [Fact]
        public void Get_ReturnListOfEntities()
        {
            // Arrange
            var categories = CollectionHelpers.GetStandardCategoryList().AsQueryable();
            MockHelpers.SetUpMockCategorySet(_dbContext, categories);

            // Act
            var result = _repository.Get();

            // Assert
            Assert.True(result != null);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void Get_ReturnIndividualEntity()
        {
            // Arrange
            var categories = CollectionHelpers.GetStandardCategoryList().AsQueryable();
            MockHelpers.SetUpMockCategorySet(_dbContext, categories);

            // Act
            var result = _repository.Get(1);

            // Assert
            Assert.True(result != null);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal("Red", result.Name);
        }

        [Fact]
        public void Add_CheckReturnsEntityWhenNotDuplicate()
        {
            // Arrange
            var entity = new Category { CategoryId = 1, Name = "Red" };

            var categories = new List<Category>
            {
                new Category { CategoryId = 2, Name = "Green" },
                new Category { CategoryId = 3, Name = "Blue" },
            }.AsQueryable();

            MockHelpers.SetUpMockCategorySet(_dbContext, categories, entity);

            // Act
            var result = _repository.Add(entity);

            // Assert
            Assert.True(result != null);
            Assert.Equal(entity.CategoryId, result.CategoryId);
        }

        [Fact]
        public void Add_CheckReturnsNullWhenDuplicate()
        {
            // Arrange
            var entity = new Category { CategoryId = 4, Name = "Red" };

            var categories = CollectionHelpers.GetStandardCategoryList().AsQueryable();

            MockHelpers.SetUpMockCategorySet(_dbContext, categories, entity);

            // Act
            var result = _repository.Add(entity);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public void Update_CheckReturnsEntityWhenNotDuplicate()
        {
            // Arrange
            var entity = new Category { CategoryId = 1, Name = "Dark Red" };

            var categories = CollectionHelpers.GetStandardCategoryList().AsQueryable();

            MockHelpers.SetUpMockCategorySet(_dbContext, categories);

            // Act
            var result = _repository.Update(entity);

            // Assert
            Assert.False(result == null);
            Assert.Equal(entity.CategoryId, result.CategoryId);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public void Update_CheckReturnsNullWhenDuplicate()
        {
            // Arrange
            var entity = new Category { CategoryId = 1, Name = "Blue" };

            var categories = CollectionHelpers.GetStandardCategoryList().AsQueryable();

            MockHelpers.SetUpMockCategorySet(_dbContext, categories);

            // Act
            var result = _repository.Add(entity);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public void Remove_CheckReturnsTrueWhenNotInUse()
        {
            // Arrange
            var entity = new Category { CategoryId = 1, Name = "Red" };

            var categories = CollectionHelpers.GetStandardCategoryList().AsQueryable();

            MockHelpers.SetUpMockCategorySet(_dbContext, categories);

            var stuff = new List<Stuff>
            {
                new Stuff { StuffId = 1, One = "One", ColourId = 1, Categories = new List<Category> { new Category { CategoryId = 2, Name = "Green" } } },
                new Stuff { StuffId = 2, One = "Two", ColourId = 2, Categories = new List<Category> { new Category { CategoryId = 2, Name = "Green" } } },
                new Stuff { StuffId = 3, One = "Three", ColourId = 3, Categories = new List<Category> { new Category { CategoryId = 3, Name = "Blue" } } }
            }.AsQueryable();

            MockHelpers.SetUpMockStuffSet(_dbContext, stuff);

            // Act 
            var result = _repository.Remove(entity);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Remove_CheckReturnsFalseWhenInUse()
        {
            // Arrange
            var entity = new Category { CategoryId = 1, Name = "Red" };

            var categories = CollectionHelpers.GetStandardCategoryList().AsQueryable();

            MockHelpers.SetUpMockCategorySet(_dbContext, categories);

            var stuff = CollectionHelpers.GetStandardStuffList().AsQueryable();

            MockHelpers.SetUpMockStuffSet(_dbContext, stuff);

            // Act 
            var result = _repository.Remove(entity);

            // Assert
            Assert.False(result);
        }
    }
}