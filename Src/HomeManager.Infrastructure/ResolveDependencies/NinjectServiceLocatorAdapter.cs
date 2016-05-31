using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace HomeManager.Infrastructure.ResolveDependencies
{
    public class NinjectServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly IKernel _container;

        public NinjectServiceLocatorAdapter(IKernel container)
        {
            _container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var instance = _container.Get(type);

            return ((IEnumerable)instance).Cast<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key != null ?
                _container.Get(serviceType, key) :
                _container.Get(serviceType);
        }
    }
}