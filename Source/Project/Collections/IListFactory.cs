using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface IListFactory
	{
		#region Methods

		IContentList<IContent> Create(ContentReference root, IListSettings settings);
		IContentList<T> Create<T>(ContentReference root, IListSettings settings) where T : IContent;
		IContentList<IContent> Create(IEnumerable<ContentReference> roots, IListSettings settings);
		IContentList<T> Create<T>(IEnumerable<ContentReference> roots, IListSettings settings) where T : IContent;

		#endregion
	}
}