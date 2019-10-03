using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using RegionOrebroLan.EPiServer.Shell.ObjectEditing.SelectionFactories;
using RegionOrebroLan.EPiServer.Web.Paging;

namespace RegionOrebroLan.EPiServer.Models.Blocks.Internal
{
	[ContentType(AvailableInEditMode = false, GUID = "f1c0ec24-8f61-4844-bcb2-66db40b938b1")]
	public class PaginationSettingsBlock : BasicBlock
	{
		#region Constructors

		public PaginationSettingsBlock() : this(ServiceLocator.Current.GetInstance<IFlagHelper>()) { }

		public PaginationSettingsBlock(IFlagHelper flagHelper)
		{
			this.FlagHelper = flagHelper ?? throw new ArgumentNullException(nameof(flagHelper));
		}

		#endregion

		#region Properties

		protected internal virtual IFlagHelper FlagHelper { get; }

		[Display(Order = 3)]
		[Range(1, 50)]
		public virtual int? MaximumNumberOfDisplayedPages { get; set; }

		[BackingType(typeof(PropertyString))]
		[Display(Order = 1)]
		[SelectMany(SelectionFactoryType = typeof(PaginationModesSelectionFactory))]
		public virtual PaginationModes Mode
		{
			get => this.FlagHelper.ConvertFromString<PaginationModes>(this.GetValue(nameof(this.Mode)) as string);
			set => this.SetValue(nameof(this.Mode), this.FlagHelper.ConvertToString(value));
		}

		[Display(Order = 2)]
		[Range(1, 1000)]
		public virtual int? PageSize { get; set; }

		#endregion
	}
}