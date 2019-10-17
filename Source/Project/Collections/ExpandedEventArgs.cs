using System;

namespace RegionOrebroLan.EPiServer.Collections
{
	public class ExpandedEventArgs : EventArgs
	{
		#region Constructors

		public ExpandedEventArgs(IContentNode node, ITreeSettings settings)
		{
			this.Node = node ?? throw new ArgumentNullException(nameof(node));
			this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

		#endregion

		#region Properties

		public virtual IContentNode Node { get; }
		public virtual ITreeSettings Settings { get; }

		#endregion
	}
}