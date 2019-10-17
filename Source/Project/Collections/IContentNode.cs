using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface IContentNode
	{
		#region Properties

		bool Active { get; }
		bool ActiveAncestor { get; }

		/// <summary>
		/// Expanded children. Can be empty even if the node have children internally.
		/// </summary>
		IEnumerable<IContentNode> Children { get; }

		/// <summary>
		/// The content-link without version information.
		/// </summary>
		ContentReference ContentLink { get; }

		/// <summary>
		/// If the node has no children it is a leaf. Returns true if the node has no children otherwise false. If the node is not a leaf the children-property can still be empty if the children are not expanded yet.
		/// </summary>
		bool Leaf { get; }

		IContentNode Parent { get; }
		IContent Value { get; }

		#endregion
	}

	/// <inheritdoc />
	public interface IContentNode<out T> : IContentNode where T : IContent
	{
		#region Properties

		/// <summary>
		/// Expanded children. Can be empty even if the node have children internally.
		/// </summary>
		new IEnumerable<IContentNode<T>> Children { get; }

		new IContentNode<T> Parent { get; }
		new T Value { get; }

		#endregion
	}
}