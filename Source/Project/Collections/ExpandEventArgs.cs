using System;

namespace RegionOrebroLan.EPiServer.Collections
{
	public class ExpandEventArgs : EventArgs
	{
		#region Constructors

		public ExpandEventArgs(IContentNode node, ITreeSettings settings)
		{
			this.Node = node ?? throw new ArgumentNullException(nameof(node));
			this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

		#endregion

		#region Properties

		public virtual bool Expand { get; set; }
		public virtual IContentNode Node { get; }
		public virtual ITreeSettings Settings { get; }

		#endregion
	}
}