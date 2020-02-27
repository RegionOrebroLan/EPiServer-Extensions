using System.Linq;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.EPiServer.Collections;
using RegionOrebroLan.EPiServer.Web.Paging;

namespace RegionOrebroLan.EPiServer.IntegrationTests.Collections
{
	[TestClass]
	public class ListFactoryTest
	{
		#region Methods

		[TestMethod]
		public void Create_Generic_IfTheRootIsTheSiteBlockFolderAndDepthIsSetToMaximum_ShouldReturnAResultWithACorrectNumberOfItems()
		{
			var listSettings = new ListSettings
			{
				Depth = int.MaxValue
			};

			var items = this.GetListFactory().Create<IContentMedia>(ContentReference.SiteBlockFolder, listSettings).Items.ToArray();

			Assert.AreEqual(22, items.Length);
		}

		[TestMethod]
		public void Create_IfIncludeRootsIsSetToTrue_ShouldReturnAResultWithTheRootIncluded()
		{
			var listSettings = new ListSettings
			{
				IncludeRoot = true
			};

			Assert.AreEqual(5, this.GetListFactory().Create(ContentReference.RootPage, listSettings).Items.Count());
		}

		[TestMethod]
		public void Create_IfThePaginationModeIsNone_ShouldReturnAResultWithAllItems()
		{
			var listSettings = new ListSettings
			{
				Depth = int.MaxValue,
				Pagination = this.CreatePaginationSettings(null, PaginationModes.None, null)
			};

			Assert.AreEqual(51, this.GetListFactory().Create(ContentReference.RootPage, listSettings).Items.Count());
		}

		[TestMethod]
		public void Create_IfThePaginationModeIsNone_ShouldReturnAResultWithAPaginationWithoutPages()
		{
			var listSettings = new ListSettings
			{
				Depth = int.MaxValue,
				Pagination = this.CreatePaginationSettings(null, PaginationModes.None, null)
			};

			var pagination = this.GetListFactory().Create(ContentReference.RootPage, listSettings).Pagination;

			Assert.IsFalse(pagination.Pages.Any());
		}

		[TestMethod]
		public void Create_IfTheRootIsTheSiteBlockFolder_ShouldReturnAResultWithACorrectNumberOfItems()
		{
			var listSettings = new ListSettings();

			Assert.AreEqual(3, this.GetListFactory().Create(ContentReference.SiteBlockFolder, listSettings).Items.Count());
		}

		[TestMethod]
		public void Create_IfTheRootIsTheSiteBlockFolderAndDepthIsSetToMaximum_ShouldReturnAResultWithACorrectNumberOfItems()
		{
			var listSettings = new ListSettings
			{
				Depth = int.MaxValue
			};

			var items = this.GetListFactory().Create(ContentReference.SiteBlockFolder, listSettings).Items.ToArray();

			Assert.AreEqual(32, items.Length);
			Assert.AreEqual(7, items.OfType<ContentFolder>().Count());
			Assert.AreEqual(22, items.OfType<IContentMedia>().Count());
			// ReSharper disable SuspiciousTypeConversion.Global
			Assert.AreEqual(3, items.OfType<BlockData>().Count());
			// ReSharper restore SuspiciousTypeConversion.Global
		}

		[TestMethod]
		public void Create_IfTheRootsParameterContainsDuplicates_AndIgnoreDuplicatesIsFalse_ShouldReturnAResultWithoutDuplicates()
		{
			var listSettings = new ListSettings
			{
				IgnoreDuplicates = false
			};

			var root = ContentReference.RootPage;

			Assert.AreEqual(4, this.GetListFactory().Create(new[] {root, root, root}, listSettings).Items.Count());
		}

		[TestMethod]
		public void Create_IfTheRootsParameterContainsDuplicates_AndIgnoreDuplicatesIsTrue_ShouldReturnAResultWithDuplicates()
		{
			var listSettings = new ListSettings
			{
				IgnoreDuplicates = true,
				Pagination = this.CreatePaginationSettings(null, PaginationModes.Bottom, int.MaxValue)
			};

			var root = ContentReference.RootPage;

			Assert.AreEqual(12, this.GetListFactory().Create(new[] {root, root, root}, listSettings).Items.Count());
		}

		//[TestMethod]
		//public void Create_Test1()
		//{
		//	var listSettings = new ListSettings
		//	{
		//		Depth = int.MaxValue,
		//		IncludeRoot = true
		//	};

		//	var listFactory = this.GetListFactory();

		//	var contentList = listFactory.Create(ContentReference.RootPage, listSettings);

		//	Assert.AreEqual(10, contentList.Items.Count());
		//	Assert.AreEqual(4, contentList.Pagination.TotalNumberOfPages);
		//}

		//[TestMethod]
		//public void Create_Test2()
		//{
		//	var listSettings = new ListSettings
		//	{
		//		Depth = int.MaxValue,
		//		IncludeRoot = true,
		//		Pagination = this.CreatePaginationSettings(int.MaxValue, PaginationModes.None, int.MaxValue)
		//	};

		//	var listFactory = this.GetListFactory();

		//	var contentList = listFactory.Create(ContentReference.RootPage, listSettings);

		//	Assert.AreEqual(33, contentList.Items.Count());
		//}

		protected internal virtual IPaginationSettings CreatePaginationSettings(int? maximumNumberOfDisplayedPages, PaginationModes mode, int? pageSize)
		{
			var paginationSettingsMock = new Mock<IPaginationSettings>();

			paginationSettingsMock.Setup(paginationSettings => paginationSettings.MaximumNumberOfDisplayedPages).Returns(maximumNumberOfDisplayedPages);
			paginationSettingsMock.Setup(paginationSettings => paginationSettings.Mode).Returns(mode);
			paginationSettingsMock.Setup(paginationSettings => paginationSettings.PageSize).Returns(pageSize);

			return paginationSettingsMock.Object;
		}

		protected internal virtual ListFactory GetListFactory()
		{
			return (ListFactory) ServiceLocator.Current.GetInstance<IListFactory>();
		}

		[TestMethod]
		public void ListFactory_ShouldBeRegisteredAsAService()
		{
			Assert.IsNotNull(this.GetListFactory());
		}

		#endregion
	}
}