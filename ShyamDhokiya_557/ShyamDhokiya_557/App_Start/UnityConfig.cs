using ShyamDhokiya_557_Repository.Repository;
using ShyamDhokiya_557_Repository.Service;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ShyamDhokiya_557
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<LoginRepository, LoginService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}