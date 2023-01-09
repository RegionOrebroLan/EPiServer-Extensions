using System;
using System.Globalization;
using System.IO;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation.AutoDiscovery;
using IntegrationTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
	[TestClass]
	public static class Global
	{
		#region Fields

		public static readonly InitializationEngine InitializationEngine = new((IServiceLocatorFactory)null, HostType.WebApplication, InitializationModule.Assemblies.AllowedAssemblies);

		// ReSharper disable PossibleNullReferenceException
		public static readonly string ProjectDirectoryPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
		// ReSharper restore PossibleNullReferenceException

		#endregion

		#region Methods

		[AssemblyCleanup]
		public static void Cleanup()
		{
			if(InitializationEngine.InitializationState == InitializationState.Initialized)
				InitializationEngine.Uninitialize();

			DatabaseHelper.DropDatabasesIfTheyExist();
		}

		[AssemblyInitialize]
		public static void Initialize(TestContext _)
		{
			DatabaseHelper.DropDatabasesIfTheyExist();

			CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");

			InitializationEngine.Initialize();
		}

		#endregion
	}
}