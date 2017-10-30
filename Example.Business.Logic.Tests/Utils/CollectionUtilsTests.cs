using Example.Business.Logic.Utils;
using Example.Business.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Example.Business.Logic.Tests.Utils
{
    public class CollectionUtilsTests
    {
        [Fact]
        public void PageResults_GetForOneItem()
        {
            // Arrange
            var expected = 1;
            var sampleColoursList = new List<Colour>();
            sampleColoursList.Add(new Colour { ColourId = 1, Name = "Red" });

            // Act
            var results = CollectionUtils.PageResults(sampleColoursList, 1, 5);

            // Assert
            Assert.Equal(expected, results.Paging.TotalRecords);
            Assert.Equal(expected, results.Results.Count());
        }

        [Fact]
        public void PageResults_GetForPageOneOfTwoPages()
        {
            // Arrange
            var expectedTotal = 5;
            var expectedReturned = 3;
            var sampleColoursList = new List<Colour>();
            sampleColoursList.Add(new Colour { ColourId = 1, Name = "Red" });
            sampleColoursList.Add(new Colour { ColourId = 2, Name = "Blue" });
            sampleColoursList.Add(new Colour { ColourId = 3, Name = "Green" });
            sampleColoursList.Add(new Colour { ColourId = 4, Name = "Pink" });
            sampleColoursList.Add(new Colour { ColourId = 5, Name = "Black" });

            // Act
            var results = CollectionUtils.PageResults(sampleColoursList, 1, 3);

            // Assert
            Assert.Equal(expectedTotal, results.Paging.TotalRecords);
            Assert.Equal(expectedReturned, results.Results.Count());
        }

        [Fact]
        public void PageResults_GetForPageTwoOfTwoPages()
        {
            // Arrange
            var expectedTotal = 5;
            var expectedReturned = 2;
            var sampleColoursList = new List<Colour>();
            sampleColoursList.Add(new Colour { ColourId = 1, Name = "Red" });
            sampleColoursList.Add(new Colour { ColourId = 2, Name = "Blue" });
            sampleColoursList.Add(new Colour { ColourId = 3, Name = "Green" });
            sampleColoursList.Add(new Colour { ColourId = 4, Name = "Pink" });
            sampleColoursList.Add(new Colour { ColourId = 5, Name = "Black" });

            // Act
            var results = CollectionUtils.PageResults(sampleColoursList, 2, 3);

            // Assert
            Assert.Equal(expectedTotal, results.Paging.TotalRecords);
            Assert.Equal(expectedReturned, results.Results.Count());
        }

        [Fact]
        public void PageResults_GetForInvalidPageWhenNoItems()
        {
            // Arrange
            var sampleColoursList = new List<Colour>();

            // Act
            var results = CollectionUtils.PageResults(sampleColoursList, 2, 3);

            // Assert
            Assert.Equal(0, results.Paging.TotalRecords);
            Assert.Empty(results.Results);
        }
    }
}