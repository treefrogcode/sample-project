using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Moq;
using Xunit;
using Example.Data.Repositories;
using Example.Business.Models.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Context;

namespace Example.Data.Tests.Repositories
{
    public class ColourRepositoryTests
    {
        private readonly Mock<ExampleContext> _dbContext;
        private readonly Mock<DbSet<Colour>> _colourSet;
        private readonly Mock<DbSet<Stuff>> _stuffSet;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<Session> _session;
        private readonly ColourRepository _repository;

        public ColourRepositoryTests()
        {
            _dbContext = new Mock<ExampleContext>();
            _colourSet = new Mock<DbSet<Colour>>();
            _stuffSet = new Mock<DbSet<Stuff>>();

            _httpContext = new Mock<HttpContextBase>();
            _session = new Mock<Session>();
            _repository = new ColourRepository(_dbContext.Object, _httpContext.Object, _session.Object);
        }

        [Fact]
        public void Get_ReturnListOfEntities()
        {
            var colours = new List<Colour>
            {
                new Colour { ColourId = 1, Name = "Red" },
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            }.AsQueryable();

            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(colours.Provider);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(colours.Expression);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(colours.ElementType);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(colours.GetEnumerator());

            _dbContext.Setup(m => m.ColourSet).Returns(_colourSet.Object);

            // Act
            var result = _repository.Get();

            // Assert
            Assert.True(result != null);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void Get_ReturnIndividualEntity()
        {
            var colours = new List<Colour>
            {
                new Colour { ColourId = 1, Name = "Red" },
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            }.AsQueryable();

            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(colours.Provider);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(colours.Expression);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(colours.ElementType);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(colours.GetEnumerator());

            _dbContext.Setup(m => m.ColourSet).Returns(_colourSet.Object);

            // Act
            var result = _repository.Get(1);

            // Assert
            Assert.True(result != null);
            Assert.Equal(1, result.ColourId);
            Assert.Equal("Red", result.Name);
        }

        [Fact]
        public void Add_CheckReturnsEntityWhenNotDuplicate()
        {
            // Arrange
            var entity = new Colour { ColourId = 1, Name = "Red" };

            var colours = new List<Colour>
            {
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            }.AsQueryable();

            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(colours.Provider);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(colours.Expression);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(colours.ElementType);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(colours.GetEnumerator());

            _colourSet.Setup(s => s.Add(entity)).Returns(entity);

            _dbContext.Setup(m => m.ColourSet).Returns(_colourSet.Object);

            // Act
            var result = _repository.Add(entity);

            // Assert
            Assert.True(result != null);
            Assert.Equal(entity.ColourId, result.ColourId);
        }

        [Fact]
        public void Add_CheckReturnsNullWhenDuplicate()
        {
            // Arrange
            var entity = new Colour { ColourId = 4, Name = "Red" };

            var colours = new List<Colour>
            {
                new Colour { ColourId = 1, Name = "Red" },
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            }.AsQueryable();

            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(colours.Provider);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(colours.Expression);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(colours.ElementType);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(colours.GetEnumerator());

            _colourSet.Setup(s => s.Add(entity)).Returns(entity);

            _dbContext.Setup(m => m.ColourSet).Returns(_colourSet.Object);

            // Act
            var result = _repository.Add(entity);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public void Update_CheckReturnsEntityWhenNotDuplicate()
        {
            // Arrange
            var entity = new Colour { ColourId = 1, Name = "Dark Red" };

            var colours = new List<Colour>
            {
                new Colour { ColourId = 1, Name = "Red" },
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            }.AsQueryable();

            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(colours.Provider);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(colours.Expression);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(colours.ElementType);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(colours.GetEnumerator());

            _dbContext.Setup(m => m.ColourSet).Returns(_colourSet.Object);

            // Act
            var result = _repository.Update(entity);

            // Assert
            Assert.False(result == null);
            Assert.Equal(entity.ColourId, result.ColourId);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public void Update_CheckReturnsNullWhenDuplicate()
        {
            // Arrange
            var entity = new Colour { ColourId = 1, Name = "Blue" };

            var colours = new List<Colour>
            {
                new Colour { ColourId = 1, Name = "Red" },
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            }.AsQueryable();

            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(colours.Provider);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(colours.Expression);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(colours.ElementType);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(colours.GetEnumerator());

            _dbContext.Setup(m => m.ColourSet).Returns(_colourSet.Object);

            // Act
            var result = _repository.Add(entity);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public void Remove_CheckReturnsTrueWhenNotInUse()
        {
            // Arrange
            var entity = new Colour { ColourId = 1, Name = "Red" };

            var colours = new List<Colour>
            {
                entity,
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            }.AsQueryable();

            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(colours.Provider);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(colours.Expression);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(colours.ElementType);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(colours.GetEnumerator());

            _dbContext.Setup(m => m.ColourSet).Returns(_colourSet.Object);

            var stuff = new List<Stuff>
            {
                new Stuff { StuffId = 1, One = "One", ColourId = 2, Categories = new List<Category>() },
                new Stuff { StuffId = 2, One = "Two", ColourId = 3, Categories = new List<Category>() },
                new Stuff { StuffId = 3, One = "Three", ColourId = 3, Categories = new List<Category>() },
            }.AsQueryable();

            _stuffSet.As<IQueryable<Stuff>>().Setup(s => s.Provider).Returns(stuff.Provider);
            _stuffSet.As<IQueryable<Stuff>>().Setup(s => s.Expression).Returns(stuff.Expression);
            _stuffSet.As<IQueryable<Stuff>>().Setup(s => s.ElementType).Returns(stuff.ElementType);
            _stuffSet.As<IQueryable<Stuff>>().Setup(s => s.GetEnumerator()).Returns(stuff.GetEnumerator());

            _dbContext.Setup(m => m.StuffSet).Returns(_stuffSet.Object);

            // Act 
            var result = _repository.Remove(entity);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Remove_CheckReturnsFalseWhenInUse()
        {
            // Arrange
            var entity = new Colour { ColourId = 1, Name = "Red" };

            var colours = new List<Colour>
            {
                entity,
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            }.AsQueryable();

            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(colours.Provider);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(colours.Expression);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(colours.ElementType);
            _colourSet.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(colours.GetEnumerator());

            _dbContext.Setup(m => m.ColourSet).Returns(_colourSet.Object);

            var stuff = new List<Stuff>
            {
                new Stuff { StuffId = 1, One = "One", ColourId = 1, Categories = new List<Category>() },
                new Stuff { StuffId = 2, One = "Two", ColourId = 2, Categories = new List<Category>() },
                new Stuff { StuffId = 3, One = "Three", ColourId = 3, Categories = new List<Category>() },
            }.AsQueryable();

            _stuffSet.As<IQueryable<Stuff>>().Setup(s => s.Provider).Returns(stuff.Provider);
            _stuffSet.As<IQueryable<Stuff>>().Setup(s => s.Expression).Returns(stuff.Expression);
            _stuffSet.As<IQueryable<Stuff>>().Setup(s => s.ElementType).Returns(stuff.ElementType);
            _stuffSet.As<IQueryable<Stuff>>().Setup(s => s.GetEnumerator()).Returns(stuff.GetEnumerator());

            _dbContext.Setup(m => m.StuffSet).Returns(_stuffSet.Object);

            // Act 
            var result = _repository.Remove(entity);

            // Assert
            Assert.False(result);
        }
    }
}