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
		public void Create_Test1()
		{
			var listSettings = new ListSettings
			{
				Depth = int.MaxValue,
				IncludeRoot = true
			};

			var listFactory = this.GetListFactory();

			var contentList = listFactory.Create(ContentReference.RootPage, listSettings);

			Assert.AreEqual(10, contentList.Items.Count());
			Assert.AreEqual(4, contentList.Pagination.TotalNumberOfPages);
		}

		[TestMethod]
		public void Create_Test2()
		{
			var listSettings = new ListSettings
			{
				Depth = int.MaxValue,
				IncludeRoot = true,
				Pagination = this.CreatePaginationSettings(int.MaxValue, PaginationModes.None, int.MaxValue)
			};

			var listFactory = this.GetListFactory();

			var contentList = listFactory.Create(ContentReference.RootPage, listSettings);

			Assert.AreEqual(33, contentList.Items.Count());
		}

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