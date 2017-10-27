using Example.Data.Repositories;
using Example.Data.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using Example.Business.Logic.Interfaces;
using Example.Business.Logic.Managers;
using System.Web;
using Example.Business.Models.Dtos;
using System.Collections.Generic;
using Example.Business.API.Managers;

namespace Example.Business.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<HttpContextBase>(new InjectionFactory(c => new HttpContextWrapper(HttpContext.Current)));

            var newSession = new Session { Values = new Dictionary<string, string>() };
            container.RegisterInstance(typeof(Session), newSession, new PerRequestLifetimeManager());

            // if repository for an owned entity, make per request lifetime
            container.RegisterType<IStuffRepository, StuffRepository>(new PerRequestLifetimeManager());

            // if repository not for an owned entity, can be a singleton
            container.RegisterType<IColourRepository, ColourRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICategoryRepository, CategoryRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITokenRepository, TokenRepository>(new ContainerControlledLifetimeManager());

            // if manager injects repository for an owned entity, make per request lifetime
            container.RegisterType<IStuffManager, StuffManager>(new PerRequestLifetimeManager());

            // if manager doesn't inject repository for an owned entity, can be a singleton
            container.RegisterType<IColourManager, ColourManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICategoryManager, CategoryManager>(new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}