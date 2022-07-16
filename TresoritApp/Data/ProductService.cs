using Microsoft.Azure.Cosmos.Table;
using TresoritApp.Models;

namespace TresoritApp.Data
{
	public class ProductService
	{
		DbManager dbManager;

		public ProductService(DbManager dbManager)
		{
			this.dbManager = dbManager;
		}

		public List<ProductModel> getPartitions()
		{
			var table = dbManager.GetTableClient();
			var query = new TableQuery<ProductModel>();
			var comments = table.ExecuteQuery(query);
			List<ProductModel> partitions = comments.GroupBy(comment => comment.PartitionKey).Select(p => p.First()).ToList();
			return partitions;
		}

		public List<ProductModel> getComemntsByProductName(string productName)
		{
			var table = dbManager.GetTableClient();
			var condition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, productName);
			var query = new TableQuery<ProductModel>().Where(condition);
			var products =  table.ExecuteQuery(query);
			List<ProductModel> ordered = products.OrderByDescending(t => t.Timestamp).ToList();
			return ordered;
		}

		public async Task createComment(string productName, string comment)
		{
			var table = dbManager.GetTableClient();

			ProductModel product = new ProductModel(productName)
			{
				Comment = comment
			};

			TableOperation insertOperation = TableOperation.Insert(product);
			await table.ExecuteAsync(insertOperation);

		}
	}
}
