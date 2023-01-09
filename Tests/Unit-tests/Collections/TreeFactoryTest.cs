using System;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.EPiServer.Collections;
using RegionOrebroLan.EPiServer.Filters;
using RegionOrebroLan.EPiServer.Web.Routing;

namespace UnitTests.Collections
{
	[TestClass]
	public class TreeFactoryTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_Generic_WithContentReferenceCollectionParameter_IfTheSettingsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ReturnValueOfPureMethodIsNotUsed
				this.CreateTreeFactory().Create<IContent>(Enumerable.Empty<ContentReference>(), null).ToArray();
				// ReSharper restore ReturnValueOfPureMethodIsNotUsed
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(this.IsSettingArgumentNullException(argumentNullException))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_Generic_WithContentReferenceParameter_IfTheSettingsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				this.CreateTreeFactory().Create<IContent>(ContentReference.EmptyReference, null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(this.IsSettingArgumentNullException(argumentNullException))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_WithContentReferenceCollectionParameter_IfTheSettingsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ReturnValueOfPureMethodIsNotUsed
				this.CreateTreeFactory().Create(Enumerable.Empty<ContentReference>(), null).ToArray();
				// ReSharper restore ReturnValueOfPureMethodIsNotUsed
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(this.IsSettingArgumentNullException(argumentNullException))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_WithContentReferenceParameter_IfTheSettingsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				this.CreateTreeFactory().Create(ContentReference.EmptyReference, null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(this.IsSettingArgumentNullException(argumentNullException))
					throw;
			}
		}

		protected internal virtual TreeFactory CreateTreeFactory()
		{
			return new TreeFactory(Mock.Of<IContentLoader>(), Mock.Of<IContentRouteHelperContext>(), Mock.Of<IFilterFacade>(), Mock.Of<ILoggerFactory>());
		}

		protected internal virtual bool IsSettingArgumentNullException(ArgumentNullException argumentNullException)
		{
			return string.Equals(argumentNullException?.ParamName, "settings", StringComparison.Ordinal);
		}

		#endregion
	}
}