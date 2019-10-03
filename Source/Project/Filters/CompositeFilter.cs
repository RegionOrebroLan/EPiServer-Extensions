using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EPiServer.Core;
using EPiServer.Filters;

namespace RegionOrebroLan.EPiServer.Filters
{
	public class CompositeFilter : ContentFilter
	{
		#region Constructors

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public CompositeFilter(IEnumerable<IContentFilter> filters)
		{
			var filterList = (filters ?? Enumerable.Empty<IContentFilter>()).ToList();

			if(filterList.Any(filter => filter == null))
				throw new ArgumentException("The filter-collection can not contain null-values.", nameof(filters));

			this.Filters = filterList;
		}

		public CompositeFilter(params IContentFilter[] filters) : this((IEnumerable<IContentFilter>) filters) { }

		#endregion

		#region Properties

		protected internal virtual IList<IContentFilter> Filters { get; }

		#endregion

		#region Methods

		public override bool ShouldFilter(IContent content)
		{
			return this.Filters.Any(filter => filter.ShouldFilter(content));
		}

		#endregion
	}
}