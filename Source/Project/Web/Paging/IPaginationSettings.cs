namespace RegionOrebroLan.EPiServer.Web.Paging
{
	public interface IPaginationSettings
	{
		#region Properties

		int? MaximumNumberOfDisplayedPages { get; }
		PaginationModes Mode { get; }
		int? PageSize { get; }

		#endregion
	}
}