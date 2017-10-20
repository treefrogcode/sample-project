﻿using Example.Business.Models.Entities;
using Example.Data.Context;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Example.Data.Repositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        protected override Token AddEntity(StuffContext entityContext, Token entity)
        {
            return entityContext.TokenSet.Add(entity);
        }

        protected override IEnumerable<Token> GetEntities(StuffContext entityContext)
        {
            return from e in entityContext.TokenSet
                   select e;
        }

        protected override Token GetEntity(StuffContext entityContext, int id)
        {
            var query = (from e in entityContext.TokenSet
                         where e.TokenId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override Token UpdateEntity(StuffContext entityContext, Token entity)
        {
            return (from e in entityContext.TokenSet
                    where e.TokenId == entity.TokenId
                    select e).FirstOrDefault();
        }
    }
}