using System.Web;

namespace RegionOrebroLan.EPiServer.Web
{
	public interface IWebFacade
	{
		#region Properties

		HttpContextBase HttpContext { get; }

		#endregion
	}
}