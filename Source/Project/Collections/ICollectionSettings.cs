using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Filters;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface ICollectionSettings
	{
		#region Properties

		IList<IComparer<IContent>> Comparers { get; }
		int? Depth { get; }
		IList<IContentFilter> Filters { get; }
		bool IgnoreDuplicates { get; }
		bool IncludeRoot { get; }

		#endregion
	}
}