using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace MyCompany.Models.Files
{
	[ContentType(GUID = "149c6cf9-98ae-4a33-be05-714844228ed5")]
	[MediaDescriptor(ExtensionString = "txt")]
	public class TextFile : MediaData
	{
		#region Properties

		[CultureSpecific]
		public virtual string Description { get; set; }

		#endregion
	}
}