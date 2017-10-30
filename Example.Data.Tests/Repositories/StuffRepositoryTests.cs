using Example.Business.Models.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Context;
using Example.Data.Repositories;
using Example.Data.Tests.Helpers;
using Moq;
using System.Linq;
using System.Web;
using Xunit;

namespace Example.Data.Tests.Repositories
{
    public class StuffRepositoryTests
    {
        private readonly Mock<ExampleContext> _dbContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<Session> _session;
        private readonly StuffRepository _repository;

        public StuffRepositoryTests()
        {
            _dbContext = new Mock<ExampleContext>();

            _httpContext = new Mock<HttpContextBase>();
            _session = new Mock<Session>();
            _repository = new StuffRepository(_dbContext.Object, _httpContext.Object, _session.Object);
        }

        [Fact]
        public void Get_ReturnListOfEntities()
        {
            // Arrange
            var stuff = CollectionHelpers.GetStandardStuffList().AsQueryable();
            MockHelpers.SetUpMockStuffSet(_dbContext, stuff);

            var tokenHeader = new System.Collections.Specialized.NameValueCollection();
            tokenHeader.Add("ApiToken", "foo");
            _httpContext.Setup(h => h.Request.Headers).Returns(tokenHeader);

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
            var stuff = CollectionHelpers.GetStandardStuffList().AsQueryable();

            MockHelpers.SetUpMockStuffSet(_dbContext, stuff);

            // Act
            var result = _repository.Get(1);

            // Assert
            Assert.True(result != null);
            Assert.Equal(1, result.StuffId);
            Assert.Equal("One", result.One);
        }

        [Fact]
        public void Add_CheckReturnsEntity()
        {
            // Arrange
            var entity = new Stuff { StuffId = 99, One = "Brand New" };

            var stuff = CollectionHelpers.GetStandardStuffList().AsQueryable();

            MockHelpers.SetUpMockStuffSet(_dbContext, stuff, entity);

            var tokenHeader = new System.Collections.Specialized.NameValueCollection();
            tokenHeader.Add("ApiToken", "foo");
            _httpContext.Setup(h => h.Request.Headers).Returns(tokenHeader);

            // Act
            var result = _repository.Add(entity);

            // Assert
            Assert.True(result != null);
            Assert.Equal(entity.StuffId, result.StuffId);
            Assert.Equal("foo", result.EntityOwner);
        }

        [Fact]
        public void Update_CheckReturnsEntity()
        {
            // Arrange
            var entity = new Stuff { StuffId = 1, One = "Updated One" };

            var stuff = CollectionHelpers.GetStandardStuffList().AsQueryable();
            MockHelpers.SetUpMockStuffSet(_dbContext, stuff);

            var categories = CollectionHelpers.GetStandardCategoryList().AsQueryable();
            MockHelpers.SetUpMockCategorySet(_dbContext, categories);

            var tokenHeader = new System.Collections.Specialized.NameValueCollection();
            tokenHeader.Add("ApiToken", "foo");
            _httpContext.Setup(h => h.Request.Headers).Returns(tokenHeader);

            // Act
            var result = _repository.Update(entity);

            // Assert
            Assert.False(result == null);
            Assert.Equal(entity.StuffId, result.StuffId);
            Assert.Equal(entity.One, result.One);
        }
    }
}