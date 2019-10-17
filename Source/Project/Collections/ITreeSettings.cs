using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	/// <inheritdoc />
	public interface ITreeSettings : ICollectionSettings
	{
		#region Properties

		ISet<ContentReference> Collapsed { get; }
		bool ExpandAll { get; }
		ISet<ContentReference> Expanded { get; }
		bool IndicateActiveContent { get; }
		int NumberOfLevelsInitiallyExpandedOnClient { get; }

		#endregion
	}
}