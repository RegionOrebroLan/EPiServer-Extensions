using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;
using RegionOrebroLan.EPiServer.Filters;
using RegionOrebroLan.EPiServer.Web.Routing;

namespace RegionOrebroLan.EPiServer.Collections
{
	public abstract class CollectionFactory
	{
		#region Constructors

		protected CollectionFactory(IContentLoader contentLoader, IContentRouteHelperContext contentRouteHelperContext, IFilterFacade filters, ILoggerFactory loggerFactory)
		{
			this.ContentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
			this.Filters = filters ?? throw new ArgumentNullException(nameof(filters));
			this.LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
			this.Logger = loggerFactory.Create(this.GetType().FullName);
			this.ContentRouteHelperContext = contentRouteHelperContext ?? throw new ArgumentNullException(nameof(contentRouteHelperContext));
		}

		#endregion

		#region Properties

		protected internal virtual IContentLoader ContentLoader { get; }
		protected internal virtual IContentRouteHelperContext ContentRouteHelperContext { get; }
		protected internal virtual IFilterFacade Filters { get; }
		protected internal ILogger Logger { get; }
		protected internal ILoggerFactory LoggerFactory { get; }

		#endregion

		#region Methods

		protected internal virtual IEnumerable<ContentReference> DuplicateHandledRoots(IEnumerable<ContentReference> roots, ICollectionSettings settings)
		{
			return roots == null ? Enumerable.Empty<ContentReference>() : !(settings?.IgnoreDuplicates ?? false) ? roots.Distinct(ContentReferenceComparer.Default) : roots;
		}

		#endregion
	}
}