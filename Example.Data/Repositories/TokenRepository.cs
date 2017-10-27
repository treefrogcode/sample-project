using Example.Business.Models.Entities;
using Example.Data.Context;
using Example.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.Data.Repositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public bool CheckTokenIsValid(string guid, bool isPublic)
        {
            var result = false;
            using (var dbContext = new ExampleContext())
            {
                var token = dbContext.TokenSet.Where(t => t.Guid.ToString() == guid).FirstOrDefault();
                if (token != null && (isPublic || !token.IsPublic))
                {
                    result = true;
                    token.LastAccessed = DateTime.Now;
                    dbContext.SaveChanges();
                }
            }

            return result;
        }


        protected override Token AddEntity(ExampleContext entityContext, Token entity)
        {
            return entityContext.TokenSet.Add(entity);
        }

        protected override IEnumerable<Token> GetEntities(ExampleContext entityContext)
        {
            return from e in entityContext.TokenSet
                   select e;
        }

        protected override Token GetEntity(ExampleContext entityContext, int id)
        {
            var query = (from e in entityContext.TokenSet
                         where e.TokenId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override Token UpdateEntity(ExampleContext entityContext, Token entity)
        {
            return (from e in entityContext.TokenSet
                    where e.TokenId == entity.TokenId
                    select e).FirstOrDefault();
        }
    }
}