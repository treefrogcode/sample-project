using Example.Business.Models.Dtos;
using Example.Business.Models.Entities;
using Example.Data.Interfaces;
using System.Collections.Generic;
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
            Token token = null;

            ITokenRepository tokenRepository = (ITokenRepository)context.Request.GetDependencyScope().GetService(typeof(ITokenRepository));

            var header = context.Request.Headers.SingleOrDefault(x => x.Key == "ApiToken");

            if (header.Value != null)
            {
                token = tokenRepository.CheckTokenIsValid(header.Value.FirstOrDefault().ToString(), Public);
            }

            if (token == null)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                var session = (Session)context.Request.GetDependencyScope().GetService(typeof(Session));
                if (session.Values == null) session.Values = new Dictionary<string, string>();
                if (!session.Values.ContainsKey("IsPublic"))
                {
                    session.Values.Add("IsPublic", token.IsPublic.ToString());
                }
            }
        }
    }
}