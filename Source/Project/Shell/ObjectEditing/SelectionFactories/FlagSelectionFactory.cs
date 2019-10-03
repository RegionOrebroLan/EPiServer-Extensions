using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Framework.Localization;

namespace RegionOrebroLan.EPiServer.Shell.ObjectEditing.SelectionFactories
{
	/// <inheritdoc />
	public abstract class FlagSelectionFactory<T> : EnumerationSelectionFactory<T> where T : Enum
	{
		#region Fields

		private IEnumerable<T> _enumerators;

		#endregion

		#region Constructors

		protected FlagSelectionFactory(IFlagHelper flagHelper, LocalizationService localizationService) : base(true, flagHelper, localizationService) { }

		#endregion

		#region Properties

		protected internal override IEnumerable<T> Enumerators => this._enumerators ?? (this._enumerators = Enum.GetValues(typeof(T)).Cast<T>().Where(value => (int) (object) value > 0));

		#endregion
	}
}