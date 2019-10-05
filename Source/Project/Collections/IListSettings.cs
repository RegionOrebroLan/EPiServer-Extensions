using RegionOrebroLan.EPiServer.Web.Paging;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface IListSettings : ICollectionSettings
	{
		#region Properties

		int? MaximumNumberOfItems { get; }
		IPaginationSettings Pagination { get; }

		#endregion
	}
}