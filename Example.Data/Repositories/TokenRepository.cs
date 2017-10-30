using Example.Business.Models.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Context;
using Example.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Data.Repositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(ExampleContext dbContext, HttpContextBase httpContext, Session session)
            : base(dbContext, httpContext, session)
        {

        }

        public Token CheckTokenIsValid(string guid, bool isPublic)
        {
            Token token = null;
            token = _dbContext.TokenSet.Where(t => t.Guid.ToString() == guid).FirstOrDefault();
            if (token != null && (isPublic || !token.IsPublic))
            {
                token.LastAccessed = DateTime.Now;
                _dbContext.SaveChanges();
            }

            return token;
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
            return GetEntity(entityContext, entity.TokenId);
        }
    }
}