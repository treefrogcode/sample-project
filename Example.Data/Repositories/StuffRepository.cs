﻿using Example.Business.Models.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Context;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Example.Data.Repositories
{
    public class StuffRepository : BaseRepository<Stuff>, IStuffRepository
    {
        public StuffRepository(ExampleContext dbContext, HttpContextBase httpContext, Session session)
            : base(dbContext, httpContext, session)
        {

        }

        public Stuff GetByOne(string one)
        {
            return (from a in _dbContext.StuffSet
                    where a.One == one
                    select a).FirstOrDefault();
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
            var result = GetEntity(entityContext, entity.StuffId);

            // Clear child lists and set source to get actual EF list
            result.Categories.Clear();
            var categorySet = entityContext.CategorySet.ToList();
            entity.Categories = entity.Categories != null ? entity.Categories : new List<Category>();
            var realCats = categorySet.Where(s => entity.Categories.Any(e => e.CategoryId == s.CategoryId)).ToList();
            entity.Categories = realCats;

            // Return result
            return result;
        }
        protected override Stuff DeleteEntity(ExampleContext entityContext, int id)
        {
            var deleteEntity = GetEntity(entityContext, id);
            if (deleteEntity == null) return null;
            deleteEntity.Categories.Clear();
            return deleteEntity;
        }
    }
}