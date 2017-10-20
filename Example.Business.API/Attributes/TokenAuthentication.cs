using Example.Data.Context;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Example.Business.API.Attributes
{
    public class TokenAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext context)
        {
            var valid = false;

            var header = context.Request.Headers.SingleOrDefault(x => x.Key == "ApiToken");

            if (header.Value != null)
            {
                // In .net core this can be injected and this becomes a service filter
                // having this here is awful I know but as it's just a demo.....
                using (var dbContext = new StuffContext())
                {
                    var token = dbContext.TokenSet.Where(t => t.Guid.ToString() == header.Value.FirstOrDefault().ToString()).FirstOrDefault();
                    if (token != null)
                    {
                        valid = true;
                        token.LastAccessed = DateTime.Now;
                        dbContext.SaveChanges();
                    }
                }
            }

            if (!valid)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}