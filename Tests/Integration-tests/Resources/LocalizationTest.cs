using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Resources
{
	[TestClass]
	public class LocalizationTest
	{
		#region Methods

		[TestMethod]
		public void EmbeddedLocalization_ShouldWorkProperly()
		{
			var localizationService = ServiceLocator.Current.GetInstance<LocalizationService>();

			Assert.AreEqual("At the bottom", localizationService.GetString("/system/editutil/paginationmodes/bottom"));
		}

		#endregion
	}
}