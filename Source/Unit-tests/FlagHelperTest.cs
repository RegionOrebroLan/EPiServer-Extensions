using System;
using EPiServer.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.EPiServer.Web.Paging;

namespace RegionOrebroLan.EPiServer.UnitTests
{
	[TestClass]
	public class FlagHelperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ConvertFromString_IfTheGenericParameterIsNotAFlag_ShouldThrowAnArgumentException()
		{
			try
			{
				new FlagHelper().ConvertFromString<FilterSortOrder>(null);
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.Message.Equals("T must have a flag-attribute.", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public void ConvertFromString_Test()
		{
			Assert.AreEqual(PaginationModes.Bottom | PaginationModes.Top, new FlagHelper().ConvertFromString<PaginationModes>("2,1"));
			Assert.AreEqual(PaginationModes.Bottom | PaginationModes.Top, new FlagHelper().ConvertFromString<PaginationModes>("2,1,2,2,2,1,1,1,3,4,5,6"));

			Assert.AreEqual(PaginationModes.Bottom, new FlagHelper().ConvertFromString<PaginationModes>("1"));
			Assert.AreEqual(PaginationModes.Bottom, new FlagHelper().ConvertFromString<PaginationModes>("1,1,1"));
			Assert.AreEqual(PaginationModes.Bottom, new FlagHelper().ConvertFromString<PaginationModes>("-1,A,B,1,1,1,3,4,5,Test"));

			Assert.AreEqual(PaginationModes.Top, new FlagHelper().ConvertFromString<PaginationModes>("2"));
			Assert.AreEqual(PaginationModes.Top, new FlagHelper().ConvertFromString<PaginationModes>("2,2,2"));
			Assert.AreEqual(PaginationModes.Top, new FlagHelper().ConvertFromString<PaginationModes>("-1,A,B,2,2,2,3,4,5,Test"));

			Assert.AreEqual(PaginationModes.None, new FlagHelper().ConvertFromString<PaginationModes>("0"));
			Assert.AreEqual(PaginationModes.None, new FlagHelper().ConvertFromString<PaginationModes>("0,0,0"));
			Assert.AreEqual(PaginationModes.None, new FlagHelper().ConvertFromString<PaginationModes>("0,0,0,3"));
			Assert.AreEqual(PaginationModes.None, new FlagHelper().ConvertFromString<PaginationModes>("-1,A,B,0,0,t"));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ConvertToString_IfTheGenericParameterIsNotAFlag_ShouldThrowAnArgumentException()
		{
			try
			{
				new FlagHelper().ConvertToString(FilterSortOrder.Alphabetical);
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.Message.Equals("T must have a flag-attribute.", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public void ConvertToString_Test()
		{
			Assert.AreEqual("1,2", new FlagHelper().ConvertToString(PaginationModes.Bottom | PaginationModes.Top));
			Assert.AreEqual("1,2", new FlagHelper().ConvertToString(PaginationModes.Bottom | PaginationModes.None | PaginationModes.Top));
			Assert.AreEqual("1,2", new FlagHelper().ConvertToString(PaginationModes.Top | PaginationModes.Bottom));

			Assert.AreEqual("1", new FlagHelper().ConvertToString(PaginationModes.Bottom));
			Assert.AreEqual("1", new FlagHelper().ConvertToString(PaginationModes.Bottom | PaginationModes.Bottom));
			Assert.AreEqual("1", new FlagHelper().ConvertToString(PaginationModes.None | PaginationModes.Bottom));

			Assert.AreEqual("2", new FlagHelper().ConvertToString(PaginationModes.Top));
			Assert.AreEqual("2", new FlagHelper().ConvertToString(PaginationModes.Top | PaginationModes.Top | PaginationModes.Top));
			Assert.AreEqual("2", new FlagHelper().ConvertToString(PaginationModes.None | PaginationModes.Top));
		}

		[TestMethod]
		public void IsFlag_Test()
		{
			Assert.IsFalse(new FlagHelper().IsFlag<FilterSortOrder>());

			Assert.IsTrue(new FlagHelper().IsFlag<PaginationModes>());
		}

		#endregion
	}
}