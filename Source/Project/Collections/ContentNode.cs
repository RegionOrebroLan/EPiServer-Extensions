using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;
using RegionOrebroLan.EPiServer.Collections.Extensions;

namespace RegionOrebroLan.EPiServer.Collections
{
	/// <inheritdoc />
	public class ContentNode<T> : IContentNode<T> where T : IContent
	{
		#region Fields

		private IEnumerable<IContentNode<T>> _children;
		private IEnumerable<T> _childrenInternal;

		#endregion

		#region Constructors

		protected internal ContentNode(ContentReference activeLink, IEnumerable<ContentReference> activeLinkAncestors, IContentLoader contentLoader, ILogger logger, ITreeSettings settings)
		{
			this.ActiveLink = activeLink;
			this.ActiveLinkAncestors = activeLinkAncestors ?? Enumerable.Empty<ContentReference>();
			this.ContentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
			this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

		public ContentNode(ContentReference activeLink, IEnumerable<ContentReference> activeLinkAncestors, IContentLoader contentLoader, ILogger logger, IContentNode<T> parent, ITreeSettings settings, T value) : this(activeLink, activeLinkAncestors, contentLoader, logger, settings)
		{
			this.ContentLink = value?.ContentLink?.ToReferenceWithoutVersion();
			this.Parent = parent ?? throw new ArgumentNullException(nameof(parent));

			if(value == null)
				throw new ArgumentNullException(nameof(value));

			this.Value = value;
		}

		#endregion

		#region Properties

		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		public virtual bool Active => this.Settings.IndicateActiveContent && !ContentReference.IsNullOrEmpty(this.ActiveLink) && this.ActiveLink.CompareToIgnoreWorkID(this.Value?.ContentLink);

		public virtual bool ActiveAncestor => this.Settings.IndicateActiveContent && this.ActiveLinkAncestors.Any(ancestor => !ContentReference.IsNullOrEmpty(ancestor) && ancestor.CompareToIgnoreWorkID(this.Value?.ContentLink));
		protected internal virtual ContentReference ActiveLink { get; }
		protected internal virtual IEnumerable<ContentReference> ActiveLinkAncestors { get; }
		IEnumerable<IContentNode> IContentNode.Children => this.Children;

		public virtual IEnumerable<IContentNode<T>> Children
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._children == null)
				{
					var children = new List<IContentNode<T>>();

					var expandEventArgs = new ExpandEventArgs(this, this.Settings) {Expand = this.Expand};

					this.Settings.OnExpandChildren(expandEventArgs);

					if(expandEventArgs.Expand)
					{
						var expandingEventArgs = new ExpandingEventArgs(this, this.Settings);

						this.Settings.OnExpandingChildren(expandingEventArgs);

						if(!expandingEventArgs.Cancel)
						{
							children.AddRange(this.ChildrenInternal.Select(child => new ContentNode<T>(this.ActiveLink, this.ActiveLinkAncestors, this.ContentLoader, this.Logger, this, this.Settings, child)));

							this.Settings.OnExpandedChildren(new ExpandedEventArgs(this, this.Settings));
						}
					}

					this._children = children.ToArray();
				}
				// ReSharper restore InvertIf

				return this._children;
			}
		}

		protected internal virtual IEnumerable<T> ChildrenInternal
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._childrenInternal == null)
				{
					var childrenInternal = new List<T>();

					if(!ContentReference.IsNullOrEmpty(this.ContentLink) && (this.Settings.Depth == null || this.Settings.Depth.Value > this.Level()))
						childrenInternal.AddRange(this.ContentLoader.GetChildren<T>(this.ContentLink).Filter(this.Settings).Sort(this.Settings));

					this._childrenInternal = childrenInternal.ToArray();
				}
				// ReSharper restore InvertIf

				return this._childrenInternal;
			}
		}

		public virtual ContentReference ContentLink { get; }
		protected internal virtual IContentLoader ContentLoader { get; }
		protected internal virtual bool Expand => this.Active || this.ActiveAncestor || this.Settings.Expand(this.ContentLink);
		public virtual bool Leaf => !this.ChildrenInternal.Any();
		protected internal virtual ILogger Logger { get; }
		IContentNode IContentNode.Parent => this.Parent;
		public virtual IContentNode<T> Parent { get; }
		protected internal virtual ITreeSettings Settings { get; }
		IContent IContentNode.Value => this.Value;
		public virtual T Value { get; }

		#endregion
	}
}