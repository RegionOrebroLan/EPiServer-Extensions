using EPiServer.Web.Routing;

namespace RegionOrebroLan.EPiServer.Web.Routing
{
	/// <summary>
	/// EPiServer.Web.Routing.IContentRouteHelper is registered with lifecycle = Hybrid (per request/thread).
	/// So if we want to use EPiServer.Web.Routing.IContentRouteHelper as a dependency in a singleton we can use this interface instead.
	/// </summary>
	public interface IContentRouteHelperContext
	{
		#region Properties

		IContentRouteHelper ContentRouteHelper { get; }

		#endregion
	}
}