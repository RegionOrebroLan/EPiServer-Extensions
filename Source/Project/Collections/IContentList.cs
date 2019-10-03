using System.Collections.Generic;
using RegionOrebroLan.Web.Paging;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface IContentList<out T>
	{
		#region Properties

		IEnumerable<T> Items { get; }
		IPagination Pagination { get; }

		#endregion
	}
}