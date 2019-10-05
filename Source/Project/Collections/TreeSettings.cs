using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	public class TreeSettings : CollectionSettings, ITreeSettings
	{
		#region Properties

		public virtual bool ExpandAll { get; set; }
		public virtual ISet<ContentReference> Expanded { get; } = new HashSet<ContentReference>(ContentReferenceComparer.IgnoreVersion);
		public virtual bool IndicateActiveContent { get; set; }

		#endregion
	}
}