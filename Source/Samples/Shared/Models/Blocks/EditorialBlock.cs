using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace MyCompany.Models.Blocks
{
	[ContentType(GUID = "1b21a121-ae78-44be-8865-043be537a48f")]
	public class EditorialBlock : SiteBlock
	{
		#region Properties

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 100)]
		public virtual XhtmlString MainBody { get; set; }

		#endregion
	}
}