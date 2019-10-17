using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;

namespace RegionOrebroLan.EPiServer.Models.Blocks.Internal
{
	public abstract class ContentTreeBlock : ContentCollectionBlock
	{
		#region Properties

		[Display(GroupName = SystemTabNames.Settings, Order = 20)]
		public virtual bool ExcludeRoots { get; set; }

		[Display(GroupName = SystemTabNames.Settings, Order = 40)]
		public virtual bool ExpandAll { get; set; }

		[Display(GroupName = SystemTabNames.Settings, Order = 50)]
		[Range(0, int.MaxValue)]
		public virtual int NumberOfLevelsInitiallyExpandedOnClient { get; set; }

		#endregion

		#region Methods

		public override void SetDefaultValues(ContentType contentType)
		{
			base.SetDefaultValues(contentType);

			this.NumberOfLevelsInitiallyExpandedOnClient = 1;
		}

		#endregion
	}
}