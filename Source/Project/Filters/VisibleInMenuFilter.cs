using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Filters
{
	public class VisibleInMenuFilter : ContentFilter
	{
		#region Methods

		public override bool ShouldFilter(IContent content)
		{
			return content is PageData pageData && !pageData.VisibleInMenu;
		}

		#endregion
	}
}