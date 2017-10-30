using Example.Business.Models.Entities;
using Example.Data.Context;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace Example.Data.Tests.Helpers
{
    public static class MockHelpers
    {
        public static void SetUpMockStuffSet(Mock<ExampleContext> context, IQueryable<Stuff> list, Stuff entity = null)
        {
            var set = new Mock<DbSet<Stuff>>();
            set.As<IQueryable<Stuff>>().Setup(s => s.Provider).Returns(list.Provider);
            set.As<IQueryable<Stuff>>().Setup(s => s.Expression).Returns(list.Expression);
            set.As<IQueryable<Stuff>>().Setup(s => s.ElementType).Returns(list.ElementType);
            set.As<IQueryable<Stuff>>().Setup(s => s.GetEnumerator()).Returns(list.GetEnumerator());
            if (entity != null)
            {
                set.Setup(s => s.Add(entity)).Returns(entity);
            }

            set.Setup(m => m.Include("Categories")).Returns(set.Object);
            set.Setup(m => m.Include("Colour")).Returns(set.Object);

            context.Setup(m => m.StuffSet).Returns(set.Object);
        }

        public static void SetUpMockColourSet(Mock<ExampleContext> context, IQueryable<Colour> list, Colour entity = null)
        {
            var set = new Mock<DbSet<Colour>>();
            set.As<IQueryable<Colour>>().Setup(s => s.Provider).Returns(list.Provider);
            set.As<IQueryable<Colour>>().Setup(s => s.Expression).Returns(list.Expression);
            set.As<IQueryable<Colour>>().Setup(s => s.ElementType).Returns(list.ElementType);
            set.As<IQueryable<Colour>>().Setup(s => s.GetEnumerator()).Returns(list.GetEnumerator());
            if (entity != null)
            {
                set.Setup(s => s.Add(entity)).Returns(entity);
            }

            context.Setup(m => m.ColourSet).Returns(set.Object);
        }

        public static void SetUpMockCategorySet(Mock<ExampleContext> context, IQueryable<Category> list, Category entity = null)
        {
            var set = new Mock<DbSet<Category>>();
            set.As<IQueryable<Category>>().Setup(s => s.Provider).Returns(list.Provider);
            set.As<IQueryable<Category>>().Setup(s => s.Expression).Returns(list.Expression);
            set.As<IQueryable<Category>>().Setup(s => s.ElementType).Returns(list.ElementType);
            set.As<IQueryable<Category>>().Setup(s => s.GetEnumerator()).Returns(list.GetEnumerator());
            if (entity != null)
            {
                set.Setup(s => s.Add(entity)).Returns(entity);
            }

            context.Setup(m => m.CategorySet).Returns(set.Object);
        }
    }
}