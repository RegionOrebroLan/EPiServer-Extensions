using System;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Moq;
using RegionOrebroLan.EPiServer.Data;
using RegionOrebroLan.Web.Paging;
using Shared.Data.SqlClient;

namespace IntegrationTests.Helpers
{
	[InitializableModule]
	public class TestInitialization : IConfigurableModule
	{
		#region Fields

		private static Action<IServiceConfigurationProvider> _serviceConfiguration;

		#endregion

		#region Properties

		public static Action<IServiceConfigurationProvider> ServiceConfiguration
		{
			get => _serviceConfiguration ??= _ => { };
			set => _serviceConfiguration = value;
		}

		#endregion

		#region Methods

		public virtual void ConfigureContainer(ServiceConfigurationContext context)
		{
			if(context == null)
				throw new ArgumentNullException(nameof(context));

			context.Services.AddSingleton<IDatabaseCreator, SqlServerLocalDatabaseCreator>();
			context.Services.AddSingleton<IPaginationFactory, PaginationFactory>();
			context.Services.AddSingleton<IPaginationValidator, PaginationValidator>();

			context.Services.AddSingleton(_ =>
			{
				var webHostingEnvironmentMock = new Mock<IWebHostingEnvironment>();

				webHostingEnvironmentMock.Setup(webHostingEnvironment => webHostingEnvironment.WebRootPath).Returns(Global.ProjectDirectoryPath);
				webHostingEnvironmentMock.Setup(webHostingEnvironment => webHostingEnvironment.WebRootVirtualPath).Returns("/");

				return webHostingEnvironmentMock.Object;
			});

			context.ConfigurationComplete += this.OnConfigurationComplete;
		}

		public virtual void Initialize(InitializationEngine context) { }

		protected internal virtual void OnConfigurationComplete(object sender, ServiceConfigurationEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException(nameof(e));

			ServiceConfiguration(e.Services);
		}

		public static void Reset()
		{
			_serviceConfiguration = null;
		}

		public virtual void Uninitialize(InitializationEngine context) { }

		#endregion
	}
}