using System;
using System.Collections.Generic;
using System.Linq;

namespace RegionOrebroLan.EPiServer.Collections.Generic
{
	public class CompositeComparer<T> : IComparer<T>
	{
		#region Constructors

		public CompositeComparer(IEnumerable<IComparer<T>> comparers)
		{
			var comparerList = (comparers ?? Enumerable.Empty<IComparer<T>>()).ToList();

			if(comparerList.Any(filter => filter == null))
				throw new ArgumentException("The comparer-collection can not contain null-values.", nameof(comparers));

			this.Comparers = comparerList;
		}

		public CompositeComparer(params IComparer<T>[] comparers) : this((IEnumerable<IComparer<T>>)comparers) { }

		#endregion

		#region Properties

		protected internal virtual IList<IComparer<T>> Comparers { get; }

		#endregion

		#region Methods

		public virtual int Compare(T x, T y)
		{
			var compare = 0;

			foreach(var comparer in this.Comparers)
			{
				compare = comparer.Compare(x, y);

				if(compare != 0)
					break;
			}

			return compare;
		}

		#endregion
	}
}