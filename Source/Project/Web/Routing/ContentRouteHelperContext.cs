using System;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace RegionOrebroLan.EPiServer.Web.Routing
{
	[ServiceConfiguration(typeof(IContentRouteHelperContext), Lifecycle = ServiceInstanceScope.Singleton)]
	public class ContentRouteHelperContext : IContentRouteHelperContext
	{
		#region Constructors

		public ContentRouteHelperContext(IServiceLocator serviceLocator)
		{
			this.ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
		}

		#endregion

		#region Properties

		public virtual IContentRouteHelper ContentRouteHelper => this.ServiceLocator.GetInstance<IContentRouteHelper>();
		protected internal virtual IServiceLocator ServiceLocator { get; }

		#endregion
	}
}