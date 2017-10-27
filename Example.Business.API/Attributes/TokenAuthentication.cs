using Example.Data.Context;
using Example.Data.Interfaces;
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
        public bool Public { get; set; }

        public override void OnAuthorization(HttpActionContext context)
        {
            var valid = false;

            var header = context.Request.Headers.SingleOrDefault(x => x.Key == "ApiToken");

            ITokenRepository tokenRepository = (ITokenRepository)context.Request.GetDependencyScope().GetService(typeof(ITokenRepository));

            if (header.Value != null)
            {
                valid = tokenRepository.CheckTokenIsValid(header.Value.FirstOrDefault().ToString(), Public);
            }

            if (!valid)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}