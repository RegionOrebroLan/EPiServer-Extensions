using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCompany.Models.Blocks;
using MyCompany.Models.Files;
using MyCompany.Models.Pages;

namespace RegionOrebroLan.EPiServer.IntegrationTests
{
	[TestClass]
	public class PrerequisiteTest
	{
		#region Methods

		[TestMethod]
		public void ContentLoader_GetChildren_IfTheGenericParameterIsIContentOrIContentData_ShouldNotMatter()
		{
			var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

			var contentDataDescendants = this.GetDescendants<IContentData>(contentLoader, ContentReference.RootPage).ToArray();

			Assert.AreEqual(32, contentDataDescendants.Length);

			Assert.AreEqual(6, contentDataDescendants.OfType<ContentFolder>().Count());
			Assert.AreEqual(3, contentDataDescendants.OfType<EditorialBlock>().Count());
			Assert.AreEqual(3, contentDataDescendants.OfType<ImageFile>().Count());
			Assert.AreEqual(14, contentDataDescendants.OfType<InformationPage>().Count());
			Assert.AreEqual(1, contentDataDescendants.Count(contentData => contentData.GetType() == typeof(PageData)));
			Assert.AreEqual(4, contentDataDescendants.OfType<TextFile>().Count());
			Assert.AreEqual(1, contentDataDescendants.OfType<StartPage>().Count());

			var contentDescendants = this.GetDescendants<IContentData>(contentLoader, ContentReference.RootPage).ToArray();

			Assert.AreEqual(32, contentDescendants.Length);

			Assert.AreEqual(6, contentDescendants.OfType<ContentFolder>().Count());
			Assert.AreEqual(3, contentDescendants.OfType<EditorialBlock>().Count());
			Assert.AreEqual(3, contentDescendants.OfType<ImageFile>().Count());
			Assert.AreEqual(14, contentDescendants.OfType<InformationPage>().Count());
			Assert.AreEqual(1, contentDescendants.Count(content => content.GetType() == typeof(PageData)));
			Assert.AreEqual(4, contentDescendants.OfType<TextFile>().Count());
			Assert.AreEqual(1, contentDescendants.OfType<StartPage>().Count());
		}

		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		protected internal virtual IEnumerable<T> GetDescendants<T>(IContentLoader contentLoader, ContentReference root) where T : IContentData
		{
			if(contentLoader == null)
				throw new ArgumentNullException(nameof(contentLoader));

			if(ContentReference.IsNullOrEmpty(root))
				yield break;

			foreach(var child in contentLoader.GetChildren<T>(root))
			{
				yield return child;

				var childLink = (child as IContent)?.ContentLink;

				if(!ContentReference.IsNullOrEmpty(childLink))
				{
					foreach(var descendant in this.GetDescendants<T>(contentLoader, childLink))
					{
						yield return descendant;
					}
				}
			}
		}

		#endregion
	}
}