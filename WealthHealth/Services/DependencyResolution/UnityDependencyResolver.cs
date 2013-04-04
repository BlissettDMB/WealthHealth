using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WealthHealth.Services.DependencyResolution
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is TypeInitializationException)
                {
                    throw ex.InnerException;
                }

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is TypeInitializationException)
                {
                    throw ex.InnerException;
                }

                return null;
            }
        }
    }
}