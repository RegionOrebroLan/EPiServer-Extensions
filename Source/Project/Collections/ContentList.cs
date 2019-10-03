using System;
using System.Collections.Generic;
using RegionOrebroLan.Web.Paging;

namespace RegionOrebroLan.EPiServer.Collections
{
	public class ContentList<T> : IContentList<T>
	{
		#region Constructors

		public ContentList(IEnumerable<T> items, IPagination pagination)
		{
			this.Items = items ?? throw new ArgumentNullException(nameof(items));
			this.Pagination = pagination ?? throw new ArgumentNullException(nameof(pagination));
		}

		#endregion

		#region Properties

		public virtual IEnumerable<T> Items { get; }
		public virtual IPagination Pagination { get; }

		#endregion
	}
}