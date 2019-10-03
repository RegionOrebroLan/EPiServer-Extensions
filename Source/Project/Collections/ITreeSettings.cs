using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface ITreeSettings : ICollectionSettings
	{
		#region Properties

		bool ExpandAll { get; }
		IList<ContentReference> Expanded { get; }
		bool IndicateActiveContent { get; }

		#endregion
	}
}