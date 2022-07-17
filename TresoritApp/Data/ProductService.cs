using Microsoft.Azure.Cosmos.Table;
using TresoritApp.Models;

namespace TresoritApp.Data
{
	public class ProductService : IProductService
	{
		private readonly IConfiguration _configuration;
		private const string TableName = "comments";

		public ProductService(IConfiguration configuration)
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

		public List<CommentModel> getProducts()
		{
			var table = GetTableClient();
			var query = new TableQuery<CommentModel>();
			var comments = table.ExecuteQuery(query);
			List<CommentModel> partitions = comments.GroupBy(comment => comment.PartitionKey).Select(p => p.First()).ToList();
			return partitions;
		}

		public List<CommentModel> getComemntsByProductName(string productName)
		{
			var table = GetTableClient();
			var condition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, productName);
			var query = new TableQuery<CommentModel>().Where(condition);
			var products =  table.ExecuteQuery(query);
			List<CommentModel> ordered = products.OrderByDescending(t => t.Timestamp).ToList();
			return ordered;
		}

		public async Task createComment(string productName, string comment)
		{
			var table = GetTableClient();
			string newID = Guid.NewGuid().ToString();

			CommentModel product = new CommentModel(productName, newID)
			{
				Comment = comment
			};

			TableOperation insertOperation = TableOperation.InsertOrMerge(product);
			await table.ExecuteAsync(insertOperation);
		}
	}
}
