using System;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.EPiServer.Filters;

namespace RegionOrebroLan.EPiServer.UnitTests.Filters
{
	[TestClass]
	public class FunctionFilterTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Usage", "CA1806:Do not ignore method results")]
		public void Constructor_IfTheFunctionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new FunctionFilter(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName.Equals("filterFunction", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_IfTheFunctionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				FunctionFilter.Create(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName.Equals("filterFunction", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public void Create_ShouldReturnAFunctionFilter()
		{
			Assert.IsTrue(FunctionFilter.Create(content => false) is FunctionFilter);
		}

		[TestMethod]
		public void ShouldFilter_ShouldInvokeThePassedFunction()
		{
			var invoked = false;

			var functionFilter = new FunctionFilter(content =>
			{
				invoked = true;
				return false;
			});

			Assert.IsFalse(invoked);

			functionFilter.ShouldFilter(Mock.Of<IContent>());

			Assert.IsTrue(invoked);
		}

		#endregion
	}
}