using System;
using EPiServer.Core;
using EPiServer.Filters;

namespace RegionOrebroLan.EPiServer.Filters
{
	public class FunctionFilter : ContentFilter
	{
		#region Constructors

		public FunctionFilter(Func<IContent, bool> filterFunction)
		{
			this.FilterFunction = filterFunction ?? throw new ArgumentNullException(nameof(filterFunction));
		}

		#endregion

		#region Properties

		protected internal virtual Func<IContent, bool> FilterFunction { get; }

		#endregion

		#region Methods

		public static IContentFilter Create(Func<IContent, bool> filterFunction)
		{
			if(filterFunction == null)
				throw new ArgumentNullException(nameof(filterFunction));

			return new FunctionFilter(filterFunction);
		}

		public override bool ShouldFilter(IContent content)
		{
			return this.FilterFunction(content);
		}

		#endregion
	}
}