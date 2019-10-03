using System.Collections.Generic;
using System.Linq;
using EPiServer.Framework.Localization;
using EPiServer.Shell.ObjectEditing;
using RegionOrebroLan.EPiServer.Web.Paging;

namespace RegionOrebroLan.EPiServer.Shell.ObjectEditing.SelectionFactories
{
	[SelectionFactoryRegistration]
	public class PaginationModesSelectionFactory : FlagSelectionFactory<PaginationModes>
	{
		#region Constructors

		public PaginationModesSelectionFactory(IFlagHelper flagHelper, LocalizationService localizationService) : base(flagHelper, localizationService) { }

		#endregion

		#region Properties

		protected internal override bool IncludeEmptySelection => false;
		protected internal override bool SortByText => false;

		#endregion

		#region Methods

		protected internal override IEnumerable<ISelectItem> Sort(IEnumerable<ISelectItem> selectItems)
		{
			return selectItems.OrderByDescending(selectItem => selectItem.Value);
		}

		#endregion
	}
}