using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace WebAPI.Infra
{
    public class CastleScope : IDependencyScope
    {
        private readonly IKernel kernel;
        private readonly IDisposable disposable;

        public CastleScope(IKernel kernel)
        {
            this.kernel = kernel;
            disposable = kernel.BeginScope();
        }

        public void Dispose()
        {
            disposable.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return kernel.HasComponent(serviceType) ? kernel.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.ResolveAll(serviceType).Cast<object>();
        }
    }
}