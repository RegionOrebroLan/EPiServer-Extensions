using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors.SelectionFactories;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Filters;
using EPiServer.Shell.ObjectEditing;
using RegionOrebroLan.EPiServer.Models.Blocks.Internal;

namespace RegionOrebroLan.EPiServer.Models.Blocks
{
	public abstract class PageListBlock : ContentListBlock
	{
		#region Properties

		[Display(GroupName = SystemTabNames.Settings, Order = 20)]
		public virtual bool IncludeRoots { get; set; }

		[AllowedTypes(typeof(PageData))]
		[CLSCompliant(false)]
		[Display(GroupName = SystemTabNames.Content, Order = 20)]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual IList<ContentReference> Roots { get; set; }

		[Display(GroupName = SystemTabNames.Settings, Order = 30)]
		[SelectOne(SelectionFactoryType = typeof(FilterSortOrderSelectionFactory))]
		public virtual FilterSortOrder SortOrder { get; set; }

		#endregion
	}
}