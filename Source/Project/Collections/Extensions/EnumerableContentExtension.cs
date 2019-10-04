using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using RegionOrebroLan.Collections.Generic;
using RegionOrebroLan.EPiServer.Filters;

namespace RegionOrebroLan.EPiServer.Collections.Extensions
{
	public static class EnumerableContentExtension
	{
		#region Methods

		public static IEnumerable<T> DuplicateHandled<T>(this IEnumerable<T> contents, ICollectionSettings settings) where T : IContent
		{
			if(contents == null)
				throw new ArgumentNullException(nameof(contents));

			// Guess this is enough because content comes from the cache and there is reference-equality.
			return !(settings?.IgnoreDuplicates ?? false) ? contents.Distinct() : contents;

			// If it is not enough then we can use this:
			//return !(settings?.IgnoreDuplicates ?? false) ? contents.GroupBy(content => content.ContentLink).Select(grouping => grouping.First()) : contents;
		}

		public static IEnumerable<T> Filter<T>(this IEnumerable<T> contents, ICollectionSettings settings) where T : IContent
		{
			if(contents == null)
				throw new ArgumentNullException(nameof(contents));

			// ReSharper disable InvertIf
			if(settings?.Filters != null && settings.Filters.Any())
			{
				var filter = new CompositeFilter(settings.Filters);

				contents = contents.Where(content => !filter.ShouldFilter(content));
			}
			// ReSharper restore InvertIf

			return contents;
		}

		public static IEnumerable<T> Sort<T>(this IEnumerable<T> contents, ICollectionSettings settings) where T : IContent
		{
			if(contents == null)
				throw new ArgumentNullException(nameof(contents));

			// ReSharper disable InvertIf
			if(settings?.Comparers != null && settings.Comparers.Any())
			{
				var comparer = new CompositeComparer<IContent>(settings.Comparers);

				contents = contents.OrderBy(content => content, comparer);
			}
			// ReSharper restore InvertIf

			return contents;
		}

		#endregion
	}
}