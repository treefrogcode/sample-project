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
    public class StuffManagerTests
    {
        private readonly StuffManager _StuffManager;
        private readonly Mock<IStuffRepository> _StuffRepository;

        private readonly List<Stuff> _Stuff;

        public StuffManagerTests()
        {
            _StuffRepository = new Mock<IStuffRepository>();
            _StuffManager = new StuffManager(_StuffRepository.Object);

            var cats = new List<Category>();
            cats.Add(new Category { CategoryId = 1, Name = "Fish" });
            cats.Add(new Category { CategoryId = 2, Name = "Dog" });
            cats.Add(new Category { CategoryId = 3, Name = "Horse" });

            _Stuff = new List<Stuff>();
            _Stuff.Add(new Stuff { StuffId = 1, One = "Red", Two = "And", Three = "Yellow", Categories = cats });
            _Stuff.Add(new Stuff { StuffId = 2, One = "Micky", Two = "And", Three = "Minnie", Categories = cats });
            _Stuff.Add(new Stuff { StuffId = 3, One = "Red", Two = "Hand", Three = "Gang", Categories = cats });
            _Stuff.Add(new Stuff { StuffId = 4, One = "Keep", Two = "On", Three = "Going", Categories = cats });
            _Stuff.Add(new Stuff { StuffId = 5, One = "Big", Two = "Blue", Three = "Whale", Categories = cats });
            _Stuff.Add(new Stuff { StuffId = 6, One = "Deep", Two = "Blue", Three = "Something", Categories = cats });
            _Stuff.Add(new Stuff { StuffId = 7, One = "Jumping", Two = "Jack", Three = "Flash", Categories = cats });
            _Stuff.Add(new Stuff { StuffId = 8, One = "Red", Two = "And", Three = "Blue", Categories = cats });
        }

        [Fact]
        public void GetStuff_GetAll()
        {
            // Arrange
            var expected = 8;
            _StuffRepository.Setup(r => r.Get()).Returns(_Stuff);

            // Act
            var results = _StuffManager.GetStuff(string.Empty, 1, 10);

            // Assert
            Assert.Equal(expected, results.Results.Count());
            Assert.Equal(expected, results.Paging.TotalRecords);
            Assert.Equal("Dog", results.Results.First().Categories.First().Name);
            Assert.Equal("Horse", results.Results.First().Categories.Last().Name);
        }

        [Fact]
        public void GetStuff_GetBySearch()
        {
            // Arrange
            var expected = 3;
            var search = "red";
            _StuffRepository.Setup(r => r.Get()).Returns(_Stuff);

            // Act
            var results = _StuffManager.GetStuff(search, 1, 10);

            // Assert
            Assert.Equal(expected, results.Results.Count());
            Assert.Equal(expected, results.Paging.TotalRecords);
            Assert.Equal("Dog", results.Results.First().Categories.First().Name);
            Assert.Equal("Horse", results.Results.First().Categories.Last().Name);
        }
    }
}