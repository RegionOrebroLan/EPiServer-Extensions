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

			return this.Create(this.GetContents<T>(root, settings), settings);
		}

		protected internal virtual IContentList<T> Create<T>(IEnumerable<T> contents, IListSettings settings) where T : IContent
		{
			contents = contents.Filter(settings).Sort(settings).Take(settings?.MaximumNumberOfItems ?? int.MaxValue).ToArray();

			var pagination = this.PaginationFactory.Create(settings?.Pagination?.MaximumNumberOfDisplayedPages ?? this.DefaultMaximumNumberOfDisplayedPages, contents.Count(), this.GetPageIndexKey(settings), settings?.Pagination?.PageSize ?? this.DefaultPageSize, this.GetUrl());

			return new ContentList<T>(contents.Skip(pagination.Skip).Take(pagination.Take), pagination);
		}

		public virtual IContentList<IContent> Create(IEnumerable<ContentReference> roots, IListSettings settings)
		{
			return this.Create<IContent>(roots, settings);
		}

		public virtual IContentList<T> Create<T>(IEnumerable<ContentReference> roots, IListSettings settings) where T : IContent
		{
			if(settings == null)
				throw new ArgumentNullException(nameof(settings));

			return this.Create(this.GetContents<T>(roots, settings), settings);
		}

		protected internal virtual IEnumerable<T> GetContents<T>(ContentReference root, IListSettings settings) where T : IContent
		{
			var contents = this.GetDescendants<T>(0, root, settings);

			if((settings?.IncludeRoot ?? false) && this.ContentLoader.TryGet<T>(root, out var rootContent))
				contents = new[] {rootContent}.Concat(contents);

			return contents;
		}

		protected internal virtual IEnumerable<T> GetContents<T>(IEnumerable<ContentReference> roots, IListSettings settings) where T : IContent
		{
			return (roots ?? Enumerable.Empty<ContentReference>()).SelectMany(root => this.GetContents<T>(root, settings));
		}

		protected internal virtual int GetDepth(IListSettings settings)
		{
			return settings?.Depth ?? this.DefaultDepth;
		}

		protected internal virtual IEnumerable<T> GetDescendants<T>(int level, ContentReference parent, IListSettings settings) where T : IContent
		{
			if(ContentReference.IsNullOrEmpty(parent))
				yield break;

			var depth = this.GetDepth(settings);

			if(level < 0 || level >= depth)
				yield break;

			foreach(var child in this.ContentLoader.GetChildren<T>(parent))
			{
				yield return child;

				foreach(var descendant in this.GetDescendants<T>(level + 1, child.ContentLink, settings))
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