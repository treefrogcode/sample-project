﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Business.Models.Entities;
using Example.Business.Models.Interfaces;

namespace Example.Data.Interfaces
{
    public interface IDataRepository
    {

    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, IIdentifiableEntity, new()
    {
        T Add(T entity);

        bool Remove(T entity);

        bool Remove(int id);

        T Update(T entity);

        IEnumerable<T> Get();

        T Get(int id);
    }
}
