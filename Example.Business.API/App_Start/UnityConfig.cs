using Example.Business.API.Managers;
using Example.Business.Logic.Interfaces;
using Example.Business.Logic.Managers;
using Example.Business.Models.Dtos;
using Example.Data.Context;
using Example.Data.Interfaces;
using Example.Data.Repositories;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using Unity.WebApi;

namespace Example.Business.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<DbContext, ExampleContext>(new PerRequestLifetimeManager());

            container.RegisterType<HttpContextBase>(new InjectionFactory(c => new HttpContextWrapper(HttpContext.Current)));

            var newSession = new Session { Values = new Dictionary<string, string>() };
            container.RegisterInstance(typeof(Session), newSession, new PerRequestLifetimeManager());

            // if repository for an owned entity, make per request lifetime (as results dependent on http headers)
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