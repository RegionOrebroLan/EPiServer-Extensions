using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace Shared.Models.Files
{
	[ContentType(GUID = "ce1486c1-a966-4077-9650-f8acb9ea543d")]
	[MediaDescriptor(ExtensionString = "gif,jpg,png")]
	public class ImageFile : ImageData
	{
		#region Properties

		[CultureSpecific]
		public virtual string AlternativeText { get; set; }

		[CultureSpecific]
		public virtual string Description { get; set; }

		#endregion
	}
}