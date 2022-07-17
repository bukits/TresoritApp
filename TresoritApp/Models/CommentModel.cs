using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TresoritApp.Models
{
	public class CommentModel : TableEntity
	{
		public CommentModel() { }

		public CommentModel(string product, string commentId)
		{
			PartitionKey = product;
			RowKey = commentId;
		}

		[Required]
		[MaxLength(500)]
		[JsonProperty(PropertyName = "comment")]
		public string Comment { get; set; }
	}
}
