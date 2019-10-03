using EPiServer.Core;
using RegionOrebroLan.EPiServer.Web.Paging;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface IListSettings : ICollectionSettings
	{
		#region Properties

		ContentReference ContentLink { get; }
		bool IgnoreDuplicates { get; }
		int? MaximumNumberOfItems { get; }
		IPaginationSettings Pagination { get; }

		#endregion
	}
}