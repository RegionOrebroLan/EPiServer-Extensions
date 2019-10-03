using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface IContentRoot : IContentNode
	{
		#region Properties

		bool Include { get; }

		#endregion
	}

	public interface IContentRoot<out T> : IContentRoot, IContentNode<T> where T : IContent { }
}