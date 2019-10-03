using EPiServer.Filters;

namespace RegionOrebroLan.EPiServer.Filters
{
	public interface IFilterFacade
	{
		#region Properties

		/// <summary>
		/// new CompositeFilter(new FilterPublished(EPiServer.Core.IPublishedStateAssessor), new FilterAccess());
		/// </summary>
		IContentFilter AccessFilter { get; }

		/// <summary>
		/// new CompositeFilter(new FilterContentForVisitor(), new VisibleInMenuFilter())
		/// </summary>
		IContentFilter NavigationFilter { get; }

		/// <summary>
		/// new FilterContentForVisitor()
		/// </summary>
		IContentFilter VisitorFilter { get; }

		#endregion
	}
}