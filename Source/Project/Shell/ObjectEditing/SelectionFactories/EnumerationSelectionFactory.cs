using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using EPiServer.Framework.Localization;
using EPiServer.Shell.ObjectEditing;

namespace RegionOrebroLan.EPiServer.Shell.ObjectEditing.SelectionFactories
{
	/// <summary>
	/// Important: The enum-type passed to this selection-factory should have a "None" value corresponding to 0 (zero) to work properly. Have to do with the rendering of the drop-down-list.
	/// The 0 (zero) value seems to be treated as null/empty or something.
	/// </summary>
	public abstract class EnumerationSelectionFactory<T> : LocalizationSelectionFactory, ISelectionFactory where T : Enum
	{
		#region Fields

		private IEnumerable<T> _enumerators;
		private string _localizationPathFormat;

		#endregion

		#region Constructors

		protected EnumerationSelectionFactory(IFlagHelper flagHelper, LocalizationService localizationService) : this(false, flagHelper, localizationService) { }

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		protected EnumerationSelectionFactory(bool flag, IFlagHelper flagHelper, LocalizationService localizationService) : base(localizationService)
		{
			if(flagHelper == null)
				throw new ArgumentNullException(nameof(flagHelper));

			var isFlag = flagHelper.IsFlag<T>();

			if(!flag && isFlag)
				throw new ArgumentException($"T can not have a flag-attribute. Inherit from \"{typeof(FlagSelectionFactory<T>)}\" instead.");

			if(flag && !isFlag)
				throw new ArgumentException("T must have a flag-attribute.");
		}

		#endregion

		#region Properties

		protected internal virtual IEnumerable<T> Enumerators => this._enumerators ?? (this._enumerators = Enum.GetValues(typeof(T)).Cast<T>());
		protected internal virtual bool IncludeEmptySelection => true;

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual string LocalizationPathFormat => this._localizationPathFormat ?? (this._localizationPathFormat = $"/system/editutil/{typeof(T).Name.ToLowerInvariant()}/{{0}}");

		protected internal virtual bool SortByText => true;

		#endregion

		#region Methods

		public virtual IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
		{
			var selectItems = new List<SelectItem>();

			if(this.IncludeEmptySelection)
				selectItems.Add(new SelectItem());

			// ReSharper disable LoopCanBeConvertedToQuery
			foreach(var enumerator in this.Enumerators)
			{
				selectItems.Add(new SelectItem
				{
					Text = this.GetText(enumerator),
					Value = this.GetValue(enumerator)
				});
			}
			// ReSharper restore LoopCanBeConvertedToQuery

			return this.Sort(selectItems).ToArray();
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual string GetText(T enumerator)
		{
			return this.LocalizationService.GetString(string.Format(CultureInfo.InvariantCulture, this.LocalizationPathFormat, enumerator.ToString().ToLowerInvariant()));
		}

		protected internal virtual object GetValue(T enumerator)
		{
			return Convert.ToInt32(enumerator, CultureInfo.InvariantCulture);
		}

		protected internal virtual IEnumerable<ISelectItem> Sort(IEnumerable<ISelectItem> selectItems)
		{
			return this.SortByText ? selectItems.OrderBy(selectItem => selectItem.Text) : selectItems;
		}

		#endregion
	}
}