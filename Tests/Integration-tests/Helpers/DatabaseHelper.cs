using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using RegionOrebroLan.EPiServer.Data;
using RegionOrebroLan.EPiServer.Data.SqlClient.Extensions;

namespace IntegrationTests.Helpers
{
	public static class DatabaseHelper
	{
		#region Methods

		public static void DropDatabasesIfTheyExist()
		{
			foreach(ConnectionStringSettings connectionSetting in ConfigurationManager.ConnectionStrings)
			{
				if(!string.Equals(connectionSetting.ProviderName, ProviderNames.SqlServer, StringComparison.OrdinalIgnoreCase))
					continue;

				var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionSetting.ConnectionString);

				if(!sqlConnectionStringBuilder.IsLocalDatabaseConnectionString())
					continue;

				// ReSharper disable ConvertToUsingDeclaration
				using(var context = new DbContext(connectionSetting.ConnectionString))
				{
					context.Database.Delete();
				}
				// ReSharper restore ConvertToUsingDeclaration
			}
		}

		#endregion
	}
}