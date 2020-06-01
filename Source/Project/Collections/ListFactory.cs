using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using RegionOrebroLan.EPiServer.Collections.Extensions;
using RegionOrebroLan.EPiServer.Filters;
using RegionOrebroLan.EPiServer.Web;
using RegionOrebroLan.EPiServer.Web.Paging;
using RegionOrebroLan.EPiServer.Web.Routing;
using RegionOrebroLan.Web.Paging;

namespace RegionOrebroLan.EPiServer.Collections
{
	[ServiceConfiguration(typeof(IListFactory), Lifecycle = ServiceInstanceScope.Singleton)]
	public class ListFactory : CollectionFactory, IListFactory
	{
		#region Fields

		private const int _defaultDepth = 1;
		private const int _defaultMaximumNumberOfDisplayedPages = 10;
		private const int _defaultPageSize = 10;

		#endregion

		#region Constructors

		public ListFactory(IContentLoader contentLoader, IContentRouteHelperContext contentRouteHelperContext, IFilterFacade filters, ILoggerFactory loggerFactory, IPaginationFactory paginationFactory, IWebFacade webFacade) : base(contentLoader, contentRouteHelperContext, filters, loggerFactory)
		{
			this.PaginationFactory = paginationFactory ?? throw new ArgumentNullException(nameof(paginationFactory));
			this.WebFacade = webFacade ?? throw new ArgumentNullException(nameof(webFacade));
		}

		#endregion

		#region Properties

		protected internal virtual int DefaultDepth => _defaultDepth;
		protected internal virtual int DefaultMaximumNumberOfDisplayedPages => _defaultMaximumNumberOfDisplayedPages;
		protected internal virtual int DefaultPageSize => _defaultPageSize;
		protected internal virtual IPaginationFactory PaginationFactory { get; }
		protected internal virtual IWebFacade WebFacade { get; }

		#endregion

		#region Methods

		public virtual IContentList<IContent> Create(ContentReference root, IListSettings settings)
		{
			return this.Create<IContent>(root, settings);
		}

		public virtual IContentList<T> Create<T>(ContentReference root, IListSettings settings) where T : IContent
		{
			if(settings == null)
				throw new ArgumentNullException(nameof(settings));

			return this.Create<T>(this.GetContents(root, settings), settings);
		}

		protected internal virtual IContentList<T> Create<T>(IEnumerable<IContent> contents, IListSettings settings) where T : IContent
		{
			var typedContents = (contents ?? Enumerable.Empty<IContent>())
				.OfType<T>()
				.DuplicateHandled(settings)
				.Filter(settings)
				.Sort(settings)
				.Take(settings?.MaximumNumberOfItems ?? int.MaxValue)
				.ToArray();

			var pagination = this.PaginationFactory.Create(settings?.Pagination?.MaximumNumberOfDisplayedPages ?? this.DefaultMaximumNumberOfDisplayedPages, typedContents.Length, this.GetPageIndexKey(settings), this.GetPageSize(settings), this.GetUrl());

			return new ContentList<T>(typedContents.Skip(pagination.Skip).Take(pagination.Take), pagination);
		}

		public virtual IContentList<IContent> Create(IEnumerable<ContentReference> roots, IListSettings settings)
		{
			return this.Create<IContent>(roots, settings);
		}

		public virtual IContentList<T> Create<T>(IEnumerable<ContentReference> roots, IListSettings settings) where T : IContent
		{
			if(settings == null)
				throw new ArgumentNullException(nameof(settings));

			return this.Create<T>(this.GetContents(roots, settings), settings);
		}

		protected internal virtual IEnumerable<IContent> GetContents(ContentReference root, IListSettings settings)
		{
			var contents = this.GetDescendants(0, root, settings);

			if((settings?.IncludeRoot ?? false) && this.ContentLoader.TryGet<IContent>(root, out var rootContent))
				contents = new[] {rootContent}.Concat(contents);

			return contents;
		}

		protected internal virtual IEnumerable<IContent> GetContents(IEnumerable<ContentReference> roots, IListSettings settings)
		{
			return this.DuplicateHandledRoots(roots, settings).SelectMany(root => this.GetContents(root, settings));
		}

		protected internal virtual int GetDepth(IListSettings settings)
		{
			return settings?.Depth ?? this.DefaultDepth;
		}

		protected internal virtual IEnumerable<IContent> GetDescendants(int level, ContentReference parent, IListSettings settings)
		{
			if(ContentReference.IsNullOrEmpty(parent))
				yield break;

			var depth = this.GetDepth(settings);

			if(level < 0 || level >= depth)
				yield break;

			foreach(var child in this.ContentLoader.GetChildren<IContent>(parent))
			{
				yield return child;

				foreach(var descendant in this.GetDescendants(level + 1, child.ContentLink, settings))
				{
					yield return descendant;
				}
			}
		}

		protected internal virtual string GetPageIndexKey(IListSettings settings)
		{
			const char separator = '-';
			var doubleSeparator = separator.ToString(CultureInfo.InvariantCulture) + separator;

			var key = (settings?.ContentLink?.ToString() ?? string.Empty).Replace('_', separator);

			while(key.Contains(doubleSeparator))
			{
				key = key.Replace(doubleSeparator, separator.ToString(CultureInfo.InvariantCulture));
			}

			var parts = new List<string>
			{
				"PageIndex"
			};

			if(!string.IsNullOrEmpty(key))
				parts.Add(key);

			return string.Join(separator.ToString(CultureInfo.InvariantCulture), parts);
		}

		protected internal virtual int GetPageSize(IListSettings settings)
		{
			return (settings?.Pagination?.Mode ?? PaginationModes.None) == PaginationModes.None ? int.MaxValue : settings?.Pagination?.PageSize ?? this.DefaultPageSize;
		}

		protected internal virtual Uri GetUrl()
		{
			var url = this.WebFacade.HttpContext?.Request?.Url;

			if(url == null)
				url = new Uri("http://localhost/");

			return url;
		}

		#endregion
	}
}