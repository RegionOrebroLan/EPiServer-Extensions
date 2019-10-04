using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors.SelectionFactories;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web.PropertyControls;
using RegionOrebroLan.EPiServer.Models.Blocks.Internal;

namespace RegionOrebroLan.EPiServer.Models.Blocks
{
	[ContentType(GUID = "2b49d59f-f9e3-43b4-b7bc-e45a3b72776e", Order = 1001)]
	public class FileListBlock : ContentListBlock
	{
		#region Properties

		[AllowedTypes(typeof(ContentFolder))]
		[CLSCompliant(false)]
		[Display(GroupName = SystemTabNames.Content, Order = 20)]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual IList<ContentReference> Roots { get; set; }

		[Display(GroupName = SystemTabNames.Settings, Order = 30)]
		[SelectOne(SelectionFactoryType = typeof(FileSortOrderSelectionFactory))]
		public virtual FileSortOrder SortOrder { get; set; }

		#endregion
	}
}