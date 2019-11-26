using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections.Extensions
{
	public static class ContentNodeExtension
	{
		#region Methods

		public static IEnumerable<IContentNode> Ancestors(this IContentNode node)
		{
			return node.AncestorsInternal();
		}

		public static IEnumerable<IContentNode<T>> Ancestors<T>(this IContentNode<T> node) where T : IContent
		{
			return node.AncestorsInternal();
		}

		private static IEnumerable<T> AncestorsInternal<T>(this T node) where T : IContentNode
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			while(node.Parent != null)
			{
				yield return (T) node.Parent;

				node = (T) node.Parent;
			}
		}

		public static IEnumerable<IContentNode> Descendants(this IContentNode node)
		{
			return node.DescendantsInternal();
		}

		public static IEnumerable<IContentNode<T>> Descendants<T>(this IContentNode<T> node) where T : IContent
		{
			return node.DescendantsInternal();
		}

		private static IEnumerable<T> DescendantsInternal<T>(this T node) where T : IContentNode
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			foreach(var child in node.Children)
			{
				yield return (T) child;

				foreach(var descendant in child.Descendants())
				{
					yield return (T) descendant;
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

		public static IContentNode Root(this IContentNode node)
		{
			return node.RootInternal();
		}

		public static IContentNode<T> Root<T>(this IContentNode<T> node) where T : IContent
		{
			return node.RootInternal();
		}

		private static T RootInternal<T>(this T node) where T : IContentNode
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			while(node.Parent != null)
			{
				node = (T) node.Parent;
			}

			return node;
		}

		#endregion
	}
}