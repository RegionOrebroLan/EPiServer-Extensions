using EPiServer.Core;
using EPiServer.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.EPiServer.Collections;

namespace RegionOrebroLan.EPiServer.IntegrationTests.Collections
{
	[TestClass]
	public class TreeFactoryTest
	{
		#region Methods

		protected internal virtual TreeFactory GetTreeFactory()
		{
			return (TreeFactory) ServiceLocator.Current.GetInstance<ITreeFactory>();
		}

		[TestMethod]
		public void TreeFactory_Create_Test()
		{
			var treeFactory = this.GetTreeFactory();

			var tree = treeFactory.Create(ContentReference.StartPage, new TreeSettings());

			Assert.IsNotNull(tree);
		}

		[TestMethod]
		public void TreeFactory_ShouldBeRegisteredAsAService()
		{
			Assert.IsNotNull(this.GetTreeFactory());
		}

		#endregion
	}
}