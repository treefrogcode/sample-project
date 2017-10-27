using Example.Business.Models.Dtos;
using Example.Business.Models.Interfaces;
using Example.Data.Context;
using System.Web;

namespace Example.Data.Repositories
{
    public abstract class BaseRepository<T> : BaseContextRepository<T, ExampleContext>
        where T : class, IIdentifiableEntity, new()
    {
        public BaseRepository(HttpContextBase httpContext, Session session) : base(httpContext, session)
        {
        }
    }
}