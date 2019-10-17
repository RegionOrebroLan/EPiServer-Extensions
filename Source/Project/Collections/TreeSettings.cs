using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	/// <inheritdoc cref="CollectionSettings" />
	/// <inheritdoc cref="ITreeSettings" />
	public class TreeSettings : CollectionSettings, ITreeSettings
	{
		#region Events

		public virtual event EventHandler<ExpandEventArgs> ExpandChildren;
		public virtual event EventHandler<ExpandedEventArgs> ExpandedChildren;
		public virtual event EventHandler<ExpandingEventArgs> ExpandingChildren;

		#endregion

		#region Properties

		public virtual ISet<ContentReference> Collapsed { get; } = new HashSet<ContentReference>(ContentReferenceComparer.IgnoreVersion);
		public virtual ISet<ContentReference> Expanded { get; } = new HashSet<ContentReference>(ContentReferenceComparer.IgnoreVersion);
		public virtual bool IndicateActiveContent { get; set; }
		public virtual bool PopulateEntireTree { get; set; }

		#endregion

		#region Methods

		public virtual void OnExpandChildren(ExpandEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException(nameof(e));

			this.ExpandChildren?.Invoke(this, e);
		}

		public virtual void OnExpandedChildren(ExpandedEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException(nameof(e));

			this.ExpandedChildren?.Invoke(this, e);
		}

		public virtual void OnExpandingChildren(ExpandingEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException(nameof(e));

			this.ExpandingChildren?.Invoke(this, e);
		}

		#endregion
	}
}