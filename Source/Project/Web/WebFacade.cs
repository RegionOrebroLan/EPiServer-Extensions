using System.Web;
using EPiServer.ServiceLocation;

namespace RegionOrebroLan.EPiServer.Web
{
	[ServiceConfiguration(typeof(IWebFacade), Lifecycle = ServiceInstanceScope.Singleton)]
	public class WebFacade : IWebFacade
	{
		#region Properties

		public virtual HttpContextBase HttpContext => System.Web.HttpContext.Current != null ? new HttpContextWrapper(System.Web.HttpContext.Current) : null;

		#endregion
	}
}