using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Shared.Models.Pages
{
	[ContentType(GUID = "2453ab73-6957-4a89-aa23-64daa07e76e6")]
	public class StartPage : SitePage
	{
		#region Properties

		[Display(GroupName = SystemTabNames.Content, Order = 400)]
		public virtual ContentArea MainArea { get; set; }

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 300)]
		public virtual XhtmlString MainBody { get; set; }

		[Display(GroupName = SystemTabNames.Content, Order = 500)]
		public virtual ContentArea RightArea { get; set; }

		#endregion
	}
}