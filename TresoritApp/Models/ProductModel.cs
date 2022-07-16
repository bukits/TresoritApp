using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TresoritApp.Models
{
	public class ProductModel : TableEntity
	{
		public ProductModel() { }

		public ProductModel(string name)
		{
			PartitionKey = name;
		}

		[Required]
		[MaxLength(500)]
		[JsonProperty(PropertyName = "comment")]
		public string Comment { get; set; }
	}
}
