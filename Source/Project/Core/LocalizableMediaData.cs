//using System.Collections.Generic;
//using System.Globalization;
//using EPiServer.Core;

//namespace RegionOrebroLan.EPiServer.Core
//{
//	public abstract class LocalizableMediaData : MediaData, ILocalizable
//	{
//		#region Properties

//		//[CultureSpecific]
//		//[UIHint(UIHint.MediaFile)]
//		////[EditorDescriptor(EditorDescriptorType = typeof(BlobEditorDescriptor))]
//		//// https://getadigital.com/blog/content-reference-selector-with-direct-file-upload/
//		//// https://world.episerver.com/forum/developer-forum/Developer-to-developer/Thread-Container/2017/5/specify-default-editor-descriptor-for-custom-property-class/
//		//// https://world.episerver.com/forum/developer-forum/Feature-requests/Thread-Container/2016/8/localization-of-mediadata/
//		//public virtual Url LocalizableFile { get; set; }
//		////public virtual Blob LocalizableBinaryData
//		////{
//		////	get => base.BinaryData;
//		////	set => base.BinaryData = value;
//		////}

//		//[CultureSpecific]
//		//public override Blob BinaryData
//		//{
//		//	get
//		//	{
//		//		var binaryData = base.BinaryData;

//		//		return binaryData;
//		//	}
//		//	set => base.BinaryData = value;
//		//}

//		//public override Uri BinaryDataContainer
//		//{
//		//	get
//		//	{
//		//		var binaryDataContainer = base.BinaryDataContainer;

//		//		return binaryDataContainer;
//		//	}
//		//}
//		public virtual IEnumerable<CultureInfo> ExistingLanguages { get; set; }
//		public virtual CultureInfo MasterLanguage { get; set; }

//		#endregion

//		//[CultureSpecific]
//		//public override Blob Thumbnail
//		//{
//		//	get
//		//	{
//		//		var thumbnail = base.Thumbnail;

//		//		return thumbnail;
//		//	}
//		//	set => base.Thumbnail = value;
//		//}
//	}
//}

