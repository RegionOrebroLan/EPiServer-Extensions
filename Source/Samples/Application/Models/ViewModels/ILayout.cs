using System;
using System.Collections.Generic;
using System.Globalization;
using MyCompany.Models.Pages;
using RegionOrebroLan.EPiServer.Collections;

namespace MyCompany.MyWebApplication.Models.ViewModels
{
	public interface ILayout
	{
		#region Properties

		CultureInfo Culture { get; }
		IDictionary<CultureInfo, Uri> CultureNavigation { get; }
		IContentRoot<SitePage> MainNavigation { get; }
		IContentRoot<SitePage> SubNavigation { get; }
		string Title { get; }

		#endregion
	}
}