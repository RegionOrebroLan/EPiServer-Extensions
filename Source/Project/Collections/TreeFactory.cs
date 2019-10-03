using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using RegionOrebroLan.EPiServer.Filters;
using RegionOrebroLan.EPiServer.Web.Routing;

namespace RegionOrebroLan.EPiServer.Collections
{
	[ServiceConfiguration(typeof(ITreeFactory), Lifecycle = ServiceInstanceScope.Singleton)]
	public class TreeFactory : CollectionFactory, ITreeFactory
	{
		#region Constructors

		public TreeFactory(IContentLoader contentLoader, IContentRouteHelperContext contentRouteHelperContext, IFilterFacade filters, ILoggerFactory loggerFactory) : base(contentLoader, contentRouteHelperContext, filters, loggerFactory) { }

		#endregion

		#region Methods

		public virtual IContentRoot<IContent> Create(ContentReference root, ITreeSettings settings)
		{
			return this.Create<IContent>(root, settings);
		}

		[SuppressMessage("Design", "CA1031:Do not catch general exception types")]
		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		public virtual IContentRoot<T> Create<T>(ContentReference root, ITreeSettings settings) where T : IContent
		{
			if(settings == null)
				throw new ArgumentNullException(nameof(settings));

			var activeLink = settings.IndicateActiveContent ? this.ContentRouteHelperContext.ContentRouteHelper.ContentLink : null;
			var activeLinkAncestors = (ContentReference.IsNullOrEmpty(activeLink) ? Enumerable.Empty<ContentReference>() : this.ContentLoader.GetAncestors(activeLink).Select(ancestor => ancestor.ContentLink)).ToArray();

			T content = default;

			if(!ContentReference.IsNullOrEmpty(root))
			{
				try
				{
					content = this.ContentLoader.Get<T>(root);

					if(this.Filters.AccessFilter.ShouldFilter(content))
					{
						content = default;

						if(this.Logger.IsWarningEnabled())
							this.Logger.Warning("The current user do not have access to root \"{0}\".", root);
					}
				}
				catch(Exception exception)
				{
					if(this.Logger.IsErrorEnabled())
						this.Logger.Error($"Could not get content for root \"{root}\".", exception);
				}
			}
			else
			{
				if(this.Logger.IsDebugEnabled())
					this.Logger.Debug("The root is {0}.", root == null ? "null" : "empty");
			}

			return new ContentRoot<T>(activeLink, activeLinkAncestors, root, this.ContentLoader, this.LoggerFactory.Create(typeof(ContentNode<T>).FullName), settings, content);
		}

		public virtual IEnumerable<IContentRoot<IContent>> Create(IEnumerable<ContentReference> roots, ITreeSettings settings)
		{
			return this.Create<IContent>(roots, settings);
		}

		public virtual IEnumerable<IContentRoot<T>> Create<T>(IEnumerable<ContentReference> roots, ITreeSettings settings) where T : IContent
		{
			if(settings == null)
				throw new ArgumentNullException(nameof(settings));

			foreach(var root in roots ?? Enumerable.Empty<ContentReference>())
			{
				yield return this.Create<T>(root, settings);
			}
		}

		#endregion
	}
}