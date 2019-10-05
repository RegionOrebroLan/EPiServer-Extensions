using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Filters;

namespace RegionOrebroLan.EPiServer.Collections
{
	public abstract class CollectionSettings : ICollectionSettings
	{
		#region Properties

		public virtual IList<IComparer<IContent>> Comparers { get; } = new List<IComparer<IContent>>();
		public virtual ContentReference ContentLink { get; set; }
		public virtual int? Depth { get; set; }
		public virtual IList<IContentFilter> Filters { get; } = new List<IContentFilter>();
		public virtual bool IgnoreDuplicates { get; set; }
		public virtual bool IncludeRoot { get; set; }

		#endregion
	}
}