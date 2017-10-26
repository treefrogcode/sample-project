using Core.Common.Utils.Utils;
using Example.Business.Models.Entities;
using Example.Business.Models.Interfaces;
using Example.Data.Interfaces;
using System;
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
                return (GetEntities(entityContext)).ToArray().ToList();
        }

        public T Get(int id)
        {
            using (U entityContext = new U())
                return GetEntity(entityContext, id);
        }
    }
}