using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Linq;
using System;
using System.Collections.Generic;
using Example.Data.Context;

namespace Example.Data.Repositories
{
    public class ColourRepository : BaseRepository<Colour>, IColourRepository
    {
        protected bool InUse(ExampleContext entityContext, Colour entity)
        {
            return entityContext.StuffSet.Any(s => s.ColourId == entity.ColourId);
        }

        protected override Colour AddEntity(ExampleContext entityContext, Colour entity)
        {
            return entityContext.ColourSet.Add(entity);
        }

        protected override IEnumerable<Colour> GetEntities(ExampleContext entityContext)
        {
                return from e in entityContext.ColourSet
                       .OrderBy(c => c.Name)
                   select e;
        }

        protected override Colour GetEntity(ExampleContext entityContext, int id)
        {
            var query = (from e in entityContext.ColourSet
                         where e.ColourId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override Colour UpdateEntity(ExampleContext entityContext, Colour entity)
        {
            return (from e in entityContext.ColourSet
                    where e.ColourId == entity.ColourId
                    select e).FirstOrDefault();
        }

        protected override bool CheckNotDuplicate(ExampleContext entityContext, Colour entity)
        {
            return !entityContext.ColourSet.Any(c => c.ColourId != entity.ColourId && c.Name.ToLower() == entity.Name.ToLower());
        }

        protected override bool CheckNotInUse(ExampleContext entityContext, int id)
        {
            return !entityContext.StuffSet.Any(s => s.ColourId == id);
        }
    }
}