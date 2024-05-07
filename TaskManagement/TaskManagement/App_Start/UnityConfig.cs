using System.Web.Mvc;
using Taskmanagement_Repository.Interface;
using Taskmanagement_Repository.Service;
using Unity;
using Unity.Mvc5;

namespace TaskManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IStateRepository, StateService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}