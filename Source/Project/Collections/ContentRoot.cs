using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;

namespace RegionOrebroLan.EPiServer.Collections
{
	public class ContentRoot<T> : ContentNode<T>, IContentRoot<T> where T : IContent
	{
		#region Constructors

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public ContentRoot(ContentReference activeLink, IEnumerable<ContentReference> activeLinkAncestors, ContentReference contentLink, IContentLoader contentLoader, ILogger logger, ITreeSettings settings, T value) : base(activeLink, activeLinkAncestors, contentLoader, logger, settings)
		{
			this.ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));

			if(ContentReference.IsNullOrEmpty(contentLink))
				throw new ArgumentException("The content-link can not be empty.", nameof(contentLink));

			this.Value = value;
		}

		#endregion

		#region Properties

		protected internal override ContentReference ContentLink { get; }
		public virtual bool Include => this.Settings.IncludeRoot && this.Value != null;
		public override IContentNode<T> Parent => null;
		public override T Value { get; }

		#endregion
	}
}