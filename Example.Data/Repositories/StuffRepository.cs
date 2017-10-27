using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Example.Data.Context;
using System.Data.Entity;

namespace Example.Data.Repositories
{
    public class StuffRepository : BaseRepository<Stuff>, IStuffRepository
    {
        public Stuff GetByOne(string one)
        {
            using (ExampleContext entityContext = new ExampleContext())
            {
                return (from a in entityContext.StuffSet
                        where a.One == one
                        select a).FirstOrDefault();
            }
        }

        protected override Stuff AddEntity(ExampleContext entityContext, Stuff entity)
        {
            // Don't re-add child objects
            if (entity.Colour != null) entityContext.Entry(entity.Colour).State = EntityState.Unchanged;

            if (entity.Categories != null)
            {
                foreach (var cat in entity.Categories)
                {
                    entityContext.Entry(cat).State = EntityState.Unchanged;
                }
            }

            return entityContext.StuffSet.Add(entity);
        }

        protected override IEnumerable<Stuff> GetEntities(ExampleContext entityContext)
        {
            var query =  from e in entityContext.StuffSet
                         .Include("Colour").Include("Categories")
                         .OrderBy(x => x.One).ThenBy(x => x.Two).ThenBy(x => x.Three)
                         select e;

            return query;
        }

        protected override Stuff GetEntity(ExampleContext entityContext, int id)
        {
            var query = (from e in entityContext.StuffSet
                         where e.StuffId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override Stuff UpdateEntity(ExampleContext entityContext, Stuff entity)
        {
            // Don't re-add child objects (will be added via foreign key property instead)
            if (entity.Colour != null) entityContext.Entry(entity.Colour).State = EntityState.Unchanged;

            // Get existing object
            var result =  (from e in entityContext.StuffSet
                    where e.StuffId == entity.StuffId
                    select e).FirstOrDefault();

            // Clear child lists and set source to get actual EF list
            result.Categories.Clear();
            var categorySet = entityContext.CategorySet.ToList();
            var realCats = categorySet.Where(s => entity.Categories.Any(e => e.CategoryId == s.CategoryId)).ToList();
            entity.Categories = realCats;

            // Return result
            return result;
        }
    }
}