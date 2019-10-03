using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.EPiServer.Web.Paging;

namespace RegionOrebroLan.EPiServer.UnitTests.Web.Paging
{
	[TestClass]
	public class PaginationModesTest
	{
		#region Methods

		[TestMethod]
		public void Cast_Test()
		{
			var paginationModes = (PaginationModes) 3;
			Assert.AreEqual("Bottom, Top", paginationModes.ToString());

			paginationModes = (PaginationModes) 73;
			Assert.AreEqual("73", paginationModes.ToString());
		}

		[TestMethod]
		public void ToString_Test()
		{
			var paginationModes = PaginationModes.None;
			Assert.AreEqual("None", paginationModes.ToString());

			paginationModes = PaginationModes.Bottom;
			Assert.AreEqual("Bottom", paginationModes.ToString());

			paginationModes = PaginationModes.Top;
			Assert.AreEqual("Top", paginationModes.ToString());

			paginationModes = PaginationModes.Bottom | PaginationModes.Top;
			Assert.AreEqual("Bottom, Top", paginationModes.ToString());

			paginationModes = PaginationModes.Top | PaginationModes.Bottom;
			Assert.AreEqual("Bottom, Top", paginationModes.ToString());

			paginationModes = PaginationModes.Bottom | PaginationModes.None | PaginationModes.Top;
			Assert.AreEqual("Bottom, Top", paginationModes.ToString());

			paginationModes = PaginationModes.Bottom | PaginationModes.Bottom | PaginationModes.Bottom;
			Assert.AreEqual("Bottom", paginationModes.ToString());
		}

		[TestMethod]
		public void TryParse_Test()
		{
			Assert.IsTrue(Enum.TryParse("3", false, out PaginationModes paginationModes));
			Assert.AreEqual("Bottom, Top", paginationModes.ToString());

			Assert.IsTrue(Enum.TryParse("73", false, out paginationModes));
			Assert.AreEqual("73", paginationModes.ToString());
		}

		#endregion
	}
}