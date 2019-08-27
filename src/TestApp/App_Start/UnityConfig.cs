using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Web;
using TestApp.Controllers;
using TestApp.Models;
using TestApp.Repositories;
using TestApp.Services.Abstractions;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TestApp
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);              
              return container;
          });
      
        public static IUnityContainer Container => container.Value;
        #endregion
        
        public static void RegisterTypes(IUnityContainer container)
        {           
            container.RegisterType<IFilmsRepository, FilmsRepository>();

            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());

            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(
                    o => HttpContext.Current.GetOwinContext().Authentication
                )
            );

        }
    }
}