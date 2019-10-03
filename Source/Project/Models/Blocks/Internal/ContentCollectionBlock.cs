using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace RegionOrebroLan.EPiServer.Models.Blocks.Internal
{
	public abstract class ContentCollectionBlock : BasicBlock
	{
		#region Properties

		[Display(GroupName = SystemTabNames.Settings, Order = 10)]
		[Range(0, int.MaxValue)]
		public virtual int? Depth { get; set; }

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 10)]
		public virtual string Heading { get; set; }

		#endregion
	}
}