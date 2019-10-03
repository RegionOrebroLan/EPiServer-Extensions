using System;
using EPiServer.Framework.Localization;

namespace RegionOrebroLan.EPiServer.Shell.ObjectEditing.SelectionFactories
{
	public abstract class LocalizationSelectionFactory
	{
		#region Constructors

		protected LocalizationSelectionFactory(LocalizationService localizationService)
		{
			this.LocalizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
		}

		#endregion

		#region Properties

		protected internal virtual LocalizationService LocalizationService { get; }

		#endregion
	}
}