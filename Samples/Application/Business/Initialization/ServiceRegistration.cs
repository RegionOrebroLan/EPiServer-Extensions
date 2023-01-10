using System;
using EPiServer.Configuration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using RegionOrebroLan.EPiServer.Data;
using RegionOrebroLan.Web.Paging;
using Shared.Data.SqlClient;

namespace MyCompany.MyWebApplication.Business.Initialization
{
	[InitializableModule]
	public class ServiceRegistration : IConfigurableModule
	{
		#region Methods

		public virtual void ConfigureContainer(ServiceConfigurationContext context)
		{
			if(context == null)
				throw new ArgumentNullException(nameof(context));

			context.Services.AddSingleton<IDatabaseCreator, SqlServerLocalDatabaseCreator>();
			context.Services.AddSingleton<IPaginationFactory, PaginationFactory>();
			context.Services.AddSingleton<IPaginationValidator, PaginationValidator>();

			context.Services.AddSingleton(Settings.Instance);
		}

		public virtual void Initialize(InitializationEngine context) { }
		public virtual void Uninitialize(InitializationEngine context) { }

		#endregion
	}
}