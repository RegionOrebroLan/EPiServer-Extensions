using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;

namespace RegionOrebroLan.EPiServer.Models.Blocks.Internal
{
	public abstract class ContentListBlock : ContentCollectionBlock
	{
		#region Properties

		[Display(GroupName = SystemTabNames.Settings, Order = 50)]
		public virtual int? MaximumNumberOfItems { get; set; }

		[Display(GroupName = SystemTabNames.Settings, Order = 40)]
		public virtual PaginationSettingsBlock Pagination { get; set; }

		#endregion
	}
}