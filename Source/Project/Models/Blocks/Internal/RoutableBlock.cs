using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Web.Routing;

namespace RegionOrebroLan.EPiServer.Models.Blocks.Internal
{
	public abstract class RoutableBlock : BasicBlock, IRoutable
	{
		#region Properties

#pragma warning disable CS0618 // Type or member is obsolete
		[UIHint("previewabletext")]
		public virtual string RouteSegment
		{
			// It does not seem possible with just:
			// get;
			// set;
			// here. Seems like "RouteSegment" is reserved somehow. So we solve it by using property "RouteSegmentInternal" to save the value. At the same time we set Scaffold = false for "RouteSegmentInternal" so it is not visible in edit-mode.
			get => this.RouteSegmentInternal;
			set => this.RouteSegmentInternal = value;
		}
#pragma warning restore CS0618 // Type or member is obsolete

		[Obsolete("This property only exist to be able to save the RouteSegment-property. Use RouteSegment instead.", false)]
		[ScaffoldColumn(false)]
		public virtual string RouteSegmentInternal { get; set; }

		#endregion
	}
}