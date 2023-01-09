using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace Shared.Models.Pages
{
	public abstract class SitePage : PageData
	{
		#region Properties

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 100)]
		[UIHint(UIHint.Textarea)]
		public virtual string MetaDescription { get; set; }

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 200)]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual IList<string> MetaKeywords { get; set; }

		#endregion
	}
}