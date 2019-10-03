using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	public class TreeSettings : CollectionSettings, ITreeSettings
	{
		#region Properties

		public virtual bool ExpandAll { get; set; }
		public virtual IList<ContentReference> Expanded { get; } = new List<ContentReference>();
		public virtual bool IndicateActiveContent { get; set; }

		#endregion
	}
}