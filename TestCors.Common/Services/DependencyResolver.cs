using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace TestCors.Common.Services
{
    public class DependencyResolver : IDependencyResolver
    {
        private Lazy<IServiceProvider> _serviceProvider;

        internal DependencyResolver()
        { }

        internal IDependencyResolver RegisterServiceProvider(Func<IServiceProvider> initializerFunction)
        {
            _serviceProvider = new Lazy<IServiceProvider>(initializerFunction);

            return this;
        }

        public TService GetService<TService>() => _serviceProvider.Value.GetService<TService>();

        public IEnumerable<TService> GetServices<TService>() => _serviceProvider.Value.GetServices<TService>();
    }
}
