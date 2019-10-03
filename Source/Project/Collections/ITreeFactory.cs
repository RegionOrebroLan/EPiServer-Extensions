using System.Collections.Generic;
using EPiServer.Core;

namespace RegionOrebroLan.EPiServer.Collections
{
	public interface ITreeFactory
	{
		#region Methods

		IContentRoot<IContent> Create(ContentReference root, ITreeSettings settings);
		IContentRoot<T> Create<T>(ContentReference root, ITreeSettings settings) where T : IContent;
		IEnumerable<IContentRoot<IContent>> Create(IEnumerable<ContentReference> roots, ITreeSettings settings);
		IEnumerable<IContentRoot<T>> Create<T>(IEnumerable<ContentReference> roots, ITreeSettings settings) where T : IContent;

		#endregion
	}
}