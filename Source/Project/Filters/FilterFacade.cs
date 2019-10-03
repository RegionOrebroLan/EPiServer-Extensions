using System;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.ServiceLocation;

namespace RegionOrebroLan.EPiServer.Filters
{
	/// <inheritdoc />
	[ServiceConfiguration(typeof(IFilterFacade), Lifecycle = ServiceInstanceScope.Singleton)]
	public class FilterFacade : IFilterFacade
	{
		#region Fields

		private IContentFilter _accessFilter;

		#endregion

		#region Constructors

		public FilterFacade(IPublishedStateAssessor publishedStateAssessor)
		{
			this.PublishedStateAssessor = publishedStateAssessor ?? throw new ArgumentNullException(nameof(publishedStateAssessor));
		}

		#endregion

		#region Properties

		public virtual IContentFilter AccessFilter => this._accessFilter ?? (this._accessFilter = new CompositeFilter(new FilterPublished(this.PublishedStateAssessor), new FilterAccess()));
		public virtual IContentFilter NavigationFilter { get; } = new CompositeFilter(new FilterContentForVisitor(), new VisibleInMenuFilter());
		protected internal virtual IPublishedStateAssessor PublishedStateAssessor { get; }
		public virtual IContentFilter VisitorFilter { get; } = new FilterContentForVisitor();

		#endregion
	}
}