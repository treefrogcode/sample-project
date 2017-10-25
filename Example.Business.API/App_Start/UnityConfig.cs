using Example.Data.Repositories;
using Example.Data.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using Example.Business.Logic.Interfaces;
using Example.Business.Logic.Managers;

namespace Example.Business.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IStuffRepository, StuffRepository>();
            container.RegisterType<IColourRepository, ColourRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ITokenRepository, TokenRepository>();

            container.RegisterType<IStuffManager, StuffManager>();
            container.RegisterType<IColourManager, ColourManager>();
            container.RegisterType<ICategoryManager, CategoryManager>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}