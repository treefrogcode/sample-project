﻿using Example.Business.Models.Dtos;
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
    public class ColourRepositoryTests
    {
        private readonly Mock<ExampleContext> _dbContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<Session> _session;
        private readonly ColourRepository _repository;

        public ColourRepositoryTests()
        {
            _dbContext = new Mock<ExampleContext>();

            _httpContext = new Mock<HttpContextBase>();
            _session = new Mock<Session>();
            _repository = new ColourRepository(_dbContext.Object, _httpContext.Object, _session.Object);
        }

        [Fact]
        public void Get_ReturnListOfEntities()
        {
            // Arrange
            var colours = CollectionHelpers.GetStandardColourList().AsQueryable();

            MockHelpers.SetUpMockColourSet(_dbContext, colours);

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
            var colours = CollectionHelpers.GetStandardColourList().AsQueryable();

            MockHelpers.SetUpMockColourSet(_dbContext, colours);

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

            MockHelpers.SetUpMockColourSet(_dbContext, colours, entity);

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

            var colours = CollectionHelpers.GetStandardColourList().AsQueryable();

            MockHelpers.SetUpMockColourSet(_dbContext, colours, entity);

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

            var colours = CollectionHelpers.GetStandardColourList().AsQueryable();

            MockHelpers.SetUpMockColourSet(_dbContext, colours);

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

            var colours = CollectionHelpers.GetStandardColourList().AsQueryable();

            MockHelpers.SetUpMockColourSet(_dbContext, colours);

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

            var colours = CollectionHelpers.GetStandardColourList().AsQueryable();

            MockHelpers.SetUpMockColourSet(_dbContext, colours);

            var stuff = new List<Stuff>
            {
                new Stuff { StuffId = 1, One = "One", ColourId = 2, Categories = new List<Category> { new Category { CategoryId = 2, Name = "Red" } } },
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
            var entity = new Colour { ColourId = 1, Name = "Red" };

            var colours = CollectionHelpers.GetStandardColourList().AsQueryable();

            MockHelpers.SetUpMockColourSet(_dbContext, colours);

            var stuff = CollectionHelpers.GetStandardStuffList().AsQueryable();

            MockHelpers.SetUpMockStuffSet(_dbContext, stuff);

            // Act 
            var result = _repository.Remove(entity);

            // Assert
            Assert.False(result);
        }
    }
}