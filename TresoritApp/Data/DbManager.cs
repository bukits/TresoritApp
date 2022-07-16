

using Microsoft.Azure.Cosmos.Table;

namespace TresoritApp.Data
{
	public class DbManager
	{
		private readonly IConfiguration _configuration;
		private const string TableName = "products";

		public DbManager(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public CloudTable GetTableClient()
		{
			CloudStorageAccount storageAccount;
			storageAccount = CloudStorageAccount.Parse(_configuration["StorageConnectionString"]);
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
			return tableClient.GetTableReference(TableName);
		}
	}
}
