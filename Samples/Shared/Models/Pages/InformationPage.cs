using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace Shared.Models.Pages
{
	[ContentType(GUID = "4a7f4132-33f1-443c-a837-996afb36bda8")]
	public class InformationPage : SitePage
	{
		#region Properties

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 300)]
		public virtual string Heading { get; set; }

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 400)]
		[UIHint(UIHint.Textarea)]
		public virtual string Introduction { get; set; }

		[Display(GroupName = SystemTabNames.Content, Order = 600)]
		public virtual ContentArea MainArea { get; set; }

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 500)]
		public virtual XhtmlString MainBody { get; set; }

		[Display(GroupName = SystemTabNames.Content, Order = 700)]
		public virtual ContentArea RightArea { get; set; }

		#endregion
	}
}