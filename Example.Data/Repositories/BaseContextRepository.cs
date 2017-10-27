using Core.Common.Utils.Utils;
using Example.Business.Models.Dtos;
using Example.Business.Models.Interfaces;
using Example.Data.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Example.Data.Repositories
{
    public abstract class BaseContextRepository<T, U> : IDataRepository<T>
        where T : class, IIdentifiableEntity, new()
        where U : DbContext, new()
    {
        protected HttpContextBase _httpContext;
        protected Session _session;

        protected string ApiToken
        {
            get
            {
                return _httpContext.Request.Headers["ApiToken"];
            }
        }

        protected BaseContextRepository(HttpContextBase httpContext, Session session)
        {
            _httpContext = httpContext;
            _session = session;
        }

        protected abstract T AddEntity(U entityContext, T entity);

        protected abstract T UpdateEntity(U entityContext, T entity);

        protected abstract IEnumerable<T> GetEntities(U entityContext);

        protected abstract T GetEntity(U entityContext, int id);

        protected virtual bool CheckNotInUse(U entityContext, int id)
        {
            return true;
        }

        protected virtual bool CheckNotDuplicate(U entityContext, T entity)
        {
            return true;
        }

        public T Add(T entity)
        {
            using (U entityContext = new U())
            {
                if (CheckNotDuplicate(entityContext, entity))
                {
                    T addedEntity = AddEntity(entityContext, entity);
                    entityContext.SaveChanges();
                    return addedEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Remove(T entity)
        {
            using (U entityContext = new U())
            {
                if (CheckNotInUse(entityContext, entity.EntityId))
                {
                    T removalEntity = GetEntity(entityContext, entity.EntityId); // ensure owned entity
                    entityContext.Entry<T>(removalEntity).State = EntityState.Deleted;
                    entityContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Remove(int id)
        {
            using (U entityContext = new U())
            {
                if (CheckNotInUse(entityContext, id))
                {
                    T entity = GetEntity(entityContext, id);
                    entityContext.Entry<T>(entity).State = EntityState.Deleted;
                    entityContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public T Update(T entity)
        {
            using (U entityContext = new U())
            {
                if (CheckNotDuplicate(entityContext, entity))
                {
                    T existingEntity = UpdateEntity(entityContext, entity);
                    SimpleMapper.PropertyMap(entity, existingEntity);
                    entityContext.SaveChanges();
                    return existingEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<T> Get()
        {
            using (U entityContext = new U())
            {
                if (FilterResultsByOwner())
                {
                    return (GetEntities(entityContext)).Where(x => EntityIsOwnedByOwner(x)).ToArray().ToList();
                }

                return (GetEntities(entityContext)).ToArray().ToList();
            }
        }

        public T Get(int id)
        {

            using (U entityContext = new U())
            {
                var result = GetEntity(entityContext, id);

                if (FilterResultsByOwner() && !EntityIsOwnedByOwner(result)) return null;

                return result;
            }
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