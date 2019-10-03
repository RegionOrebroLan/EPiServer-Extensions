using EPiServer.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.EPiServer.Web.Routing;

namespace RegionOrebroLan.EPiServer.IntegrationTests.Web.Routing
{
	[TestClass]
	public class ContentRouteHelperContextTest
	{
		#region Methods

		[TestMethod]
		public void ContentRouteHelper_Content_IfTheContextIsThisIntegrationTest_ShouldReturnNull()
		{
			Assert.IsNull(this.GetContentRouteHelperContext().ContentRouteHelper.Content);
		}

		[TestMethod]
		public void ContentRouteHelper_ContentLink_IfTheContextIsThisIntegrationTest_ShouldReturnNull()
		{
			Assert.IsNull(this.GetContentRouteHelperContext().ContentRouteHelper.ContentLink);
		}

		[TestMethod]
		public void ContentRouteHelper_IfTheContextIsThisIntegrationTest_ShouldReturnAnInstance()
		{
			Assert.IsNotNull(this.GetContentRouteHelperContext().ContentRouteHelper);
		}

		[TestMethod]
		public void ContentRouteHelper_LanguageID_IfTheContextIsThisIntegrationTest_ShouldReturnNull()
		{
			Assert.IsNull(this.GetContentRouteHelperContext().ContentRouteHelper.LanguageID);
		}

		[TestMethod]
		public void ContentRouteHelperContext_IfTheContextIsThisIntegrationTest_ShouldAlwaysReturnTheSameInstance()
		{
			Assert.AreEqual(this.GetContentRouteHelperContext().ContentRouteHelper, this.GetContentRouteHelperContext().ContentRouteHelper);
		}

		[TestMethod]
		public void ContentRouteHelperContext_ShouldBeRegisteredAsAService()
		{
			Assert.IsNotNull(this.GetContentRouteHelperContext());
		}

		protected internal virtual ContentRouteHelperContext GetContentRouteHelperContext()
		{
			return (ContentRouteHelperContext) ServiceLocator.Current.GetInstance<IContentRouteHelperContext>();
		}

		#endregion
	}
}