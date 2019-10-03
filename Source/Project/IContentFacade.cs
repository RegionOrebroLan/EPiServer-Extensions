using EPiServer;
using EPiServer.Web;
using EPiServer.Web.Routing;
using RegionOrebroLan.EPiServer.Collections;
using RegionOrebroLan.EPiServer.Filters;
using RegionOrebroLan.EPiServer.Web;
using RegionOrebroLan.EPiServer.Web.Routing;

namespace RegionOrebroLan.EPiServer
{
	public interface IContentFacade
	{
		#region Properties

		ICollectionFacade Collections { get; }
		IFilterFacade Filters { get; }
		IContentLoader Loader { get; }
		IContentRepository Repository { get; }
		IContentRouteHelperContext Routing { get; }
		ISiteDefinitionResolver SiteDefinitionResolver { get; }
		IUrlResolver UrlResolver { get; }
		IWebFacade Web { get; }

		#endregion
	}
}