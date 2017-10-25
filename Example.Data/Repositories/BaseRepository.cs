using Example.Business.Models.Entities;
using Example.Business.Models.Interfaces;
using Example.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Data.Repositories
{
    public abstract class BaseRepository<T> : BaseContextRepository<T, ExampleContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}