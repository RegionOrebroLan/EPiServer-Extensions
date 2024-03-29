using Shared.Models.Pages;

namespace MyCompany.MyWebApplication.Models.ViewModels
{
	public interface IPageViewModel<out T> where T : SitePage
	{
		#region Properties

		ILayout Layout { get; }
		T Page { get; }

		#endregion
	}
}