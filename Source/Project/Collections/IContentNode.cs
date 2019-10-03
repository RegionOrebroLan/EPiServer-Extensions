﻿using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface IContentNode
	{
		#region Properties

		bool Active { get; }
		bool ActiveAncestor { get; }
		IEnumerable<IContentNode> Children { get; }
		bool Leaf { get; }
		IContentNode Parent { get; }
		IContent Value { get; }

		#endregion
	}

	public interface IContentNode<out T> : IContentNode where T : IContent
	{
		#region Properties

		new IEnumerable<IContentNode<T>> Children { get; }
		new IContentNode<T> Parent { get; }
		new T Value { get; }

		#endregion
	}
}