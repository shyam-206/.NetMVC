using MVCCrud_Repository.Interface;
using MVCCrud_Repository.Service;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MVCCrud
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProduct, ProductService>();
            container.RegisterType<IRegister, RegisterService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}