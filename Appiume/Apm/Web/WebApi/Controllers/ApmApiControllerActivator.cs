using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.Web.WebApi.Controllers
{
    /// <summary>
    /// This class is used to use IOC system to create api controllers.
    /// It's used by ASP.NET system.
    /// </summary>
    public class ApmApiControllerActivator : IHttpControllerActivator
    {
        private readonly IIocResolver _iocResolver;

        public ApmApiControllerActivator(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controllerWrapper = _iocResolver.ResolveAsDisposable<IHttpController>(controllerType);
            request.RegisterForDispose(controllerWrapper);
            return controllerWrapper.Object;
        }
    }
}