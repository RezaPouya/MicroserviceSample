namespace IdentityManagment.ConfigurationModels
{
	public class RabbitMqConfigurationModel
	{
		public bool IsEnabled { get; set; }
		public ConnectionConfigurationModel Connection { get; set; }
		public EventBusConfigurationModel EventBus { get; set; }
		public class ConnectionConfigurationModel
		{
			public string UserName { get; set; }
			public string Password { get; set; }
			public string HostName { get; set; }
			public int? Port { get; set; }
		}

		public class EventBusConfigurationModel
		{
			public string ClientName { get; set; }
			public string ExchangeName { get; set; }
		}
	}
}