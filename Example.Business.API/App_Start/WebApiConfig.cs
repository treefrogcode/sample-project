using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Example.Business.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}
