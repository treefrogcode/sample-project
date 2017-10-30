using Example.Business.Models.Entities;
using System.Collections.Generic;

namespace Example.Data.Tests.Helpers
{
    public static class CollectionHelpers
    {
        public static List<Category> GetStandardCategoryList()
        {
            return new List<Category>
            {
                new Category { CategoryId = 1, Name = "Red" },
                new Category { CategoryId = 2, Name = "Green" },
                new Category { CategoryId = 3, Name = "Blue" },
            };
        }

        public static List<Colour> GetStandardColourList()
        {
            return new List<Colour>
            {
                new Colour { ColourId = 1, Name = "Red" },
                new Colour { ColourId = 2, Name = "Green" },
                new Colour { ColourId = 3, Name = "Blue" },
            };
        }

        public static List<Stuff> GetStandardStuffList()
        {
            return new List<Stuff>
            {
                new Stuff { StuffId = 1, One = "One", ColourId = 1, Categories = new List<Category> { new Category { CategoryId = 1, Name = "Red" } } },
                new Stuff { StuffId = 2, One = "Two", ColourId = 2, Categories = new List<Category> { new Category { CategoryId = 2, Name = "Green" } } },
                new Stuff { StuffId = 3, One = "Three", ColourId = 3, Categories = new List<Category> { new Category { CategoryId = 3, Name = "Blue" } } }
            };
        }
    }
}