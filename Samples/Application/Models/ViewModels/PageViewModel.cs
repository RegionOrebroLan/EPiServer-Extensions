using System;
using EPiServer.Configuration;
using EPiServer.ServiceLocation;
using RegionOrebroLan.EPiServer;
using Shared.Models.Pages;

namespace MyCompany.MyWebApplication.Models.ViewModels
{
	public class PageViewModel<T> : IPageViewModel<T> where T : SitePage
	{
		#region Fields

		private ILayout _layout;

		#endregion

		#region Constructors

		public PageViewModel(T page)
		{
			this.Page = page ?? throw new ArgumentNullException(nameof(page));
		}

		#endregion

		#region Properties

		public virtual ILayout Layout => this._layout ?? (this._layout = new Layout(ServiceLocator.Current.GetInstance<IContentFacade>(), this.Page, ServiceLocator.Current.GetInstance<Settings>()));
		public virtual T Page { get; }

		#endregion
	}
}