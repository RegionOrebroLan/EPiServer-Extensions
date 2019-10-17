using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	/// <inheritdoc cref="CollectionSettings" />
	/// <inheritdoc cref="ITreeSettings" />
	public class TreeSettings : CollectionSettings, ITreeSettings
	{
		#region Properties

		public virtual ISet<ContentReference> Collapsed { get; } = new HashSet<ContentReference>(ContentReferenceComparer.IgnoreVersion);
		public virtual bool ExpandAll { get; set; }
		public virtual ISet<ContentReference> Expanded { get; } = new HashSet<ContentReference>(ContentReferenceComparer.IgnoreVersion);
		public virtual bool IndicateActiveContent { get; set; }
		public virtual int NumberOfLevelsInitiallyExpandedOnClient { get; set; } = 1;

		#endregion
	}
}