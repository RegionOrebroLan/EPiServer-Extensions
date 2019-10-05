using RegionOrebroLan.EPiServer.Web.Paging;

namespace RegionOrebroLan.EPiServer.Collections
{
	public class ListSettings : CollectionSettings, IListSettings
	{
		#region Properties

		public virtual int? MaximumNumberOfItems { get; set; }
		public virtual IPaginationSettings Pagination { get; set; }

		#endregion
	}
}