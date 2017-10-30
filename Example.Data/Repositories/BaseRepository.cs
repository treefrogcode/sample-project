using Core.Common.Utils.Utils;
using Example.Business.Models.Dtos;
using Example.Business.Models.Interfaces;
using Example.Data.Context;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Example.Data.Repositories
{
    public abstract class BaseRepository<T> : IDataRepository<T>
        where T : class, IIdentifiableEntity, new()
    {
        protected ExampleContext _dbContext;
        protected HttpContextBase _httpContext;
        protected Session _session;

        protected string ApiToken
        {
            get
            {
                return _httpContext.Request.Headers["ApiToken"];
            }
        }

        protected BaseRepository(ExampleContext dbContext, HttpContextBase httpContext, Session session)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
            _session = session;
        }

        protected abstract T AddEntity(ExampleContext entityContext, T entity);

        protected abstract T UpdateEntity(ExampleContext entityContext, T entity);

        protected abstract IEnumerable<T> GetEntities(ExampleContext entityContext);

        protected abstract T GetEntity(ExampleContext entityContext, int id);

        protected virtual bool CheckNotInUse(ExampleContext entityContext, int id)
        {
            return true;
        }

        protected virtual bool CheckNotDuplicate(ExampleContext entityContext, T entity)
        {
            return true;
        }

        protected virtual T DeleteEntity(ExampleContext entityContext, int id)
        {
            return GetEntity(entityContext, id);
        }

        public T Add(T entity)
        {
            if (SetOwnerToToken(entity))
            {
                (entity as IOwnedEntity).EntityOwner = ApiToken;
            }

            if (CheckNotDuplicate(_dbContext, entity))
            {
                T addedEntity = AddEntity(_dbContext, entity);

                _dbContext.SaveChanges();
                return addedEntity;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(T entity)
        {
            if (CheckNotInUse(_dbContext, entity.EntityId))
            {
                T removalEntity = DeleteEntity(_dbContext, entity.EntityId); // ensure owned entity
                if (removalEntity != null)
                {
                    _dbContext.Entry<T>(removalEntity).State = EntityState.Deleted;
                    _dbContext.SaveChanges();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            if (CheckNotInUse(_dbContext, id))
            {
                T entity = DeleteEntity(_dbContext, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public T Update(T entity)
        {
            if (CheckNotDuplicate(_dbContext, entity))
            {
                T existingEntity = UpdateEntity(_dbContext, entity);
                SimpleMapper.PropertyMap(entity, existingEntity);
                _dbContext.SaveChanges();
                return existingEntity;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<T> Get()
        {
            if (FilterResultsByOwner())
            {
                return (GetEntities(_dbContext)).Where(x => EntityIsOwnedByOwner(x)).ToArray().ToList();
            }

            return (GetEntities(_dbContext)).ToArray().ToList();
        }

        public T Get(int id)
        {

            var result = GetEntity(_dbContext, id);

            if (FilterResultsByOwner() && !EntityIsOwnedByOwner(result)) return null;

            return result;
        }

        private bool SetOwnerToToken(T entity)
        {
            return typeof(IOwnedEntity).IsAssignableFrom(typeof(T)) && string.IsNullOrWhiteSpace((entity as IOwnedEntity).EntityOwner);
        }

        private bool EntityIsOwnedByOwner(T entity)
        {
            return (entity as IOwnedEntity).EntityOwner == ApiToken;
        }

        private bool FilterResultsByOwner()
        {
            var isPublic = _session.Values != null && _session.Values.ContainsKey("IsPublic") && _session.Values["IsPublic"] == true.ToString();

            if (isPublic && typeof(IOwnedEntity).IsAssignableFrom(typeof(T)))
            {
                return true;
            }

            return false;
        }
    }
}