namespace RegionOrebroLan.EPiServer.Collections
{
	public interface ICollectionFacade
	{
		#region Properties

		IListFactory ListFactory { get; }
		ITreeFactory TreeFactory { get; }

		#endregion
	}
}