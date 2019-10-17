using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	/// <inheritdoc />
	public interface ITreeSettings : ICollectionSettings
	{
		#region Events

		event EventHandler<ExpandEventArgs> ExpandChildren;
		event EventHandler<ExpandedEventArgs> ExpandedChildren;
		event EventHandler<ExpandingEventArgs> ExpandingChildren;

		#endregion

		#region Properties

		ISet<ContentReference> Collapsed { get; }
		ISet<ContentReference> Expanded { get; }
		bool IndicateActiveContent { get; }
		bool PopulateEntireTree { get; }

		#endregion

		#region Methods

		void OnExpandChildren(ExpandEventArgs e);
		void OnExpandedChildren(ExpandedEventArgs e);
		void OnExpandingChildren(ExpandingEventArgs e);

		#endregion
	}
}