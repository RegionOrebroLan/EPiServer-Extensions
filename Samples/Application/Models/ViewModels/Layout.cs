using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using EPiServer.Configuration;
using EPiServer.Core;
using EPiServer.Web.Routing;
using RegionOrebroLan.EPiServer;
using RegionOrebroLan.EPiServer.Collections;
using Shared.Models.Pages;

namespace MyCompany.MyWebApplication.Models.ViewModels
{
	[SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
	public class Layout : ILayout
	{
		#region Fields

		private IDictionary<CultureInfo, Uri> _cultureNavigation;
		private IContentRoot<SitePage> _mainNavigation;
		private Lazy<StartPage> _startPage;
		private Lazy<ContentReference> _startPageLink;
		private IContentRoot<SitePage> _subNavigation;

		#endregion

		#region Constructors

		public Layout(IContentFacade contentFacade, SitePage page, Settings settings)
		{
			this.ContentFacade = contentFacade ?? throw new ArgumentNullException(nameof(contentFacade));
			this.Page = page ?? throw new ArgumentNullException(nameof(page));
			this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

		#endregion

		#region Properties

		protected internal virtual IContentFacade ContentFacade { get; }
		public virtual CultureInfo Culture => this.Page.Language;

		public virtual IDictionary<CultureInfo, Uri> CultureNavigation
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._cultureNavigation == null)
				{
					var cultureNavigation = new Dictionary<CultureInfo, Uri>();

					if(this.Settings.UIShowGlobalizationUserInterface)
					{
						foreach(var culture in this.Page.ExistingLanguages)
						{
							cultureNavigation.Add(culture, new Uri(this.ContentFacade.UrlResolver.GetUrl(this.Page.ContentLink.ToReferenceWithoutVersion(), culture.Name), UriKind.RelativeOrAbsolute));
						}
					}

					this._cultureNavigation = cultureNavigation;
				}
				// ReSharper restore InvertIf

				return this._cultureNavigation;
			}
		}

		public virtual IContentRoot<SitePage> MainNavigation => this._mainNavigation ?? (this._mainNavigation = this.ContentFacade.Collections.TreeFactory.Create<SitePage>(this.StartPageLink, this.CreateMainNavigationSettings()));
		protected internal virtual SitePage Page { get; }
		protected internal virtual Settings Settings { get; }

		protected internal virtual StartPage StartPage
		{
			get
			{
				if(this._startPage == null)
				{
					this._startPage = new Lazy<StartPage>(() =>
					{
						// ReSharper disable InvertIf
						if(!ContentReference.IsNullOrEmpty(this.StartPageLink) && this.ContentFacade.Loader.TryGet(this.StartPageLink, out StartPage startPage))
						{
							if(startPage != null && !this.ContentFacade.Filters.NavigationFilter.ShouldFilter(startPage))
								return startPage;
						}
						// ReSharper restore InvertIf

						return null;
					});
				}

				return this._startPage.Value;
			}
		}

		protected internal virtual ContentReference StartPageLink
		{
			get
			{
				if(this._startPageLink == null)
					this._startPageLink = new Lazy<ContentReference>(() => this.ContentFacade.SiteDefinitionResolver.GetByContent(this.Page?.ContentLink, true)?.StartPage);

				return this._startPageLink.Value;
			}
		}

		public virtual IContentRoot<SitePage> SubNavigation
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._subNavigation == null)
				{
					ContentReference subNavigationRoot = null;

					if(!ContentReference.IsNullOrEmpty(this.StartPageLink))
					{
						// We can check some property on the content to get the root-container for the sub-menu.

						var ancestors = this.ContentFacade.Loader.GetAncestors(this.Page.ContentLink).ToArray();

						for(var i = 0; i < ancestors.Length; i++)
						{
							var ancestor = ancestors[i];

							if(ancestor.ContentLink.CompareToIgnoreWorkID(this.StartPageLink))
							{
								subNavigationRoot = i == 0 ? this.Page.ContentLink : ancestors[i - 1].ContentLink;
								break;
							}
						}
					}
					// ReSharper restore InvertIf

					this._subNavigation = this.ContentFacade.Collections.TreeFactory.Create<SitePage>(subNavigationRoot, this.CreateSubNavigationSettings());
				}

				return this._subNavigation;
			}
		}

		public virtual string Title => this.Page.Name + " • My company";

		#endregion

		#region Methods

		protected internal virtual TreeSettings CreateMainNavigationSettings()
		{
			var mainNavigationSettings = this.CreateNavigationSettings();

			mainNavigationSettings.Depth = 1;
			mainNavigationSettings.IncludeRoot = true;

			return mainNavigationSettings;
		}

		protected internal virtual TreeSettings CreateNavigationSettings()
		{
			var navigationSettings = new TreeSettings
			{
				IndicateActiveContent = true
			};

			navigationSettings.Filters.Add(this.ContentFacade.Filters.NavigationFilter);

			return navigationSettings;
		}

		protected internal virtual TreeSettings CreateSubNavigationSettings()
		{
			var subNavigationSettings = this.CreateNavigationSettings();

			return subNavigationSettings;
		}

		#endregion
	}
}