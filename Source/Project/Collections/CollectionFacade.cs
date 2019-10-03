using System;
using EPiServer.ServiceLocation;

namespace RegionOrebroLan.EPiServer.Collections
{
	[ServiceConfiguration(typeof(ICollectionFacade), Lifecycle = ServiceInstanceScope.Singleton)]
	public class CollectionFacade : ICollectionFacade
	{
		#region Constructors

		public CollectionFacade(IListFactory listFactory, ITreeFactory treeFactory)
		{
			this.ListFactory = listFactory ?? throw new ArgumentNullException(nameof(listFactory));
			this.TreeFactory = treeFactory ?? throw new ArgumentNullException(nameof(treeFactory));
		}

		#endregion

		#region Properties

		public virtual IListFactory ListFactory { get; }
		public virtual ITreeFactory TreeFactory { get; }

		#endregion
	}
}