using Example.Business.Logic.Managers;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using Example.Business.Models.Entities;

namespace Example.Business.Logic.Tests.Managers
{
    public class ColourManagerTests
    {
        private readonly ColourManager _colourManager;
        private readonly Mock<IColourRepository> _colourRepository;

        private readonly List<Colour> _colours;

        public ColourManagerTests()
        {
            _colourRepository = new Mock<IColourRepository>();
            _colourManager = new ColourManager(_colourRepository.Object);

            _colours = new List<Colour>();
            _colours.Add(new Colour { ColourId = 1, Name = "Red" });
            _colours.Add(new Colour { ColourId = 2, Name = "Blue" });
            _colours.Add(new Colour { ColourId = 3, Name = "Dark Red" });
            _colours.Add(new Colour { ColourId = 4, Name = "Teal" });
            _colours.Add(new Colour { ColourId = 5, Name = "Green" });
            _colours.Add(new Colour { ColourId = 6, Name = "Black" });
        }

        [Fact]
        public void GetColours_GetAll()
        {
            // Arrange
            var expected = 6;
            _colourRepository.Setup(r => r.Get()).Returns(_colours);

            // Act
            var results = _colourManager.GetColours(string.Empty, 1, 10);

            // Assert
            Assert.Equal(expected, results.Results.Count());
            Assert.Equal(expected, results.Paging.TotalRecords);
        }

        [Fact]
        public void GetColours_GetOneBySearch()
        {
            // Arrange
            var expected = 2;
            var search = "red";
            _colourRepository.Setup(r => r.Get()).Returns(_colours);

            // Act
            var results = _colourManager.GetColours(search, 1, 10);

            // Assert
            Assert.Equal(expected, results.Results.Count());
            Assert.Contains(search, results.Results.ToList()[0].Name.ToLower());
            Assert.Contains(search, results.Results.ToList()[1].Name.ToLower());
            Assert.Equal(expected, results.Paging.TotalRecords);
        }
    }
}