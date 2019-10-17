using System;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections.Extensions
{
	public static class TreeSettingsExtension
	{
		#region Methods

		public static bool Expand(this ITreeSettings settings, ContentReference contentLink)
		{
			if(settings == null)
				throw new ArgumentNullException(nameof(settings));

			return !ContentReference.IsNullOrEmpty(contentLink) && (settings.ExpandAll || settings.Expanded.Contains(contentLink)) && !settings.Collapsed.Contains(contentLink);
		}

		#endregion
	}
}