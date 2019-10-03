using System;
using EPiServer;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using RegionOrebroLan.EPiServer.Collections;
using RegionOrebroLan.EPiServer.Filters;
using RegionOrebroLan.EPiServer.Web;
using RegionOrebroLan.EPiServer.Web.Routing;

namespace RegionOrebroLan.EPiServer
{
	[ServiceConfiguration(typeof(IContentFacade), Lifecycle = ServiceInstanceScope.Singleton)]
	public class ContentFacade : IContentFacade
	{
		#region Constructors

		public ContentFacade(ICollectionFacade collections, IFilterFacade filters, IContentLoader loader, IContentRepository repository, IContentRouteHelperContext routing, ISiteDefinitionResolver siteDefinitionResolver, IUrlResolver urlResolver, IWebFacade web)
		{
			this.Collections = collections ?? throw new ArgumentNullException(nameof(collections));
			this.Filters = filters ?? throw new ArgumentNullException(nameof(filters));
			this.Loader = loader ?? throw new ArgumentNullException(nameof(loader));
			this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
			this.Routing = routing ?? throw new ArgumentNullException(nameof(routing));
			this.SiteDefinitionResolver = siteDefinitionResolver ?? throw new ArgumentNullException(nameof(siteDefinitionResolver));
			this.UrlResolver = urlResolver ?? throw new ArgumentNullException(nameof(urlResolver));
			this.Web = web ?? throw new ArgumentNullException(nameof(web));
		}

		#endregion

		#region Properties

		public virtual ICollectionFacade Collections { get; }
		public virtual IFilterFacade Filters { get; }
		public virtual IContentLoader Loader { get; }
		public virtual IContentRepository Repository { get; }
		public virtual IContentRouteHelperContext Routing { get; }
		public virtual ISiteDefinitionResolver SiteDefinitionResolver { get; }
		public virtual IUrlResolver UrlResolver { get; }
		public virtual IWebFacade Web { get; }

		#endregion
	}
}