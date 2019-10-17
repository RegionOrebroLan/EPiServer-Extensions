using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	/// <inheritdoc />
	public interface IContentRoot : IContentNode
	{
		#region Properties

		bool Include { get; }

		#endregion
	}

	/// <inheritdoc cref="IContentRoot" />
	/// <inheritdoc cref="IContentNode{T}" />
	public interface IContentRoot<out T> : IContentRoot, IContentNode<T> where T : IContent { }
}