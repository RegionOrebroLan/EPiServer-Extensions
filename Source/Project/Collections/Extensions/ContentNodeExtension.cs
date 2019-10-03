using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections.Extensions
{
	public static class ContentNodeExtension
	{
		#region Methods

		public static IEnumerable<IContentNode<T>> Ancestors<T>(this IContentNode<T> node) where T : IContent
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			while(node.Parent != null)
			{
				yield return node.Parent;

				node = node.Parent;
			}
		}

		public static IEnumerable<IContentNode<T>> Descendants<T>(this IContentNode<T> node) where T : IContent
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			foreach(var child in node.Children)
			{
				yield return child;

				foreach(var descendant in child.Descendants())
				{
					yield return descendant;
				}
			}
		}

		public static bool IsEmpty(this IContentNode node)
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			return node.Leaf && (!(node is IContentRoot contentRoot) || !contentRoot.Include);
		}

		public static bool IsNullOrEmpty(this IContentNode node)
		{
			return node == null || node.IsEmpty();
		}

		public static int Level(this IContentNode node)
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			var level = 0;

			while(node.Parent != null)
			{
				level++;

				node = node.Parent;
			}

			return level;
		}

		public static IContentNode<T> Root<T>(this IContentNode<T> node) where T : IContent
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			while(node.Parent != null)
			{
				node = node.Parent;
			}

			return node;
		}

		#endregion
	}
}