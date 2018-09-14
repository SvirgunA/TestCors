using System.Collections.Generic;

namespace TestCors.Common.Services
{
    public interface IDependencyResolver
    {
        TService GetService<TService>();
        IEnumerable<TService> GetServices<TService>();
    }
}
