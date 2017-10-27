﻿using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Example.Data.Context;
using System.Web;
using Example.Business.Models.Dtos;

namespace Example.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(HttpContextBase httpContext, Session session) : base(httpContext, session)
        {

        }

        protected override Category AddEntity(ExampleContext entityContext, Category entity)
        {
            return entityContext.CategorySet.Add(entity);
        }

        protected override IEnumerable<Category> GetEntities(ExampleContext entityContext)
        {
            return from e in entityContext.CategorySet
                   orderby e.Name
                   select e;
        }

        protected override Category GetEntity(ExampleContext entityContext, int id)
        {
            var query = (from e in entityContext.CategorySet
                         where e.CategoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override Category UpdateEntity(ExampleContext entityContext, Category entity)
        {
            return GetEntity(entityContext, entity.CategoryId);
        }

        protected override bool CheckNotDuplicate(ExampleContext entityContext, Category entity)
        {
            return !entityContext.CategorySet.Any(c => c.CategoryId != entity.CategoryId && c.Name.ToLower() == entity.Name.ToLower());
        }

        protected override bool CheckNotInUse(ExampleContext entityContext, int id)
        {
            return !entityContext.StuffSet.Any(s => s.Categories.Any(c => c.CategoryId == id));
        }
    }
}