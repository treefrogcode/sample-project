using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Linq;
using System;
using System.Collections.Generic;
using Example.Data.Context;

namespace Example.Data.Repositories
{
    public class StuffRepository : BaseRepository<Stuff>, IStuffRepository
    {
        public Stuff GetByOne(string one)
        {
            using (StuffContext entityContext = new StuffContext())
            {
                return (from a in entityContext.StuffSet
                        where a.One == one
                        select a).FirstOrDefault();
            }
        }

        protected override Stuff AddEntity(StuffContext entityContext, Stuff entity)
        {
            return entityContext.StuffSet.Add(entity);
        }

        protected override IEnumerable<Stuff> GetEntities(StuffContext entityContext)
        {
            return from e in entityContext.StuffSet 
                   select e;
        }

        protected override Stuff GetEntity(StuffContext entityContext, int id)
        {
            var query = (from e in entityContext.StuffSet
                         where e.StuffId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override Stuff UpdateEntity(StuffContext entityContext, Stuff entity)
        {
            return (from e in entityContext.StuffSet
                    where e.StuffId == entity.StuffId
                    select e).FirstOrDefault();
        }
    }
}