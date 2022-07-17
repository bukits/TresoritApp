using TresoritApp.Models;

namespace TresoritApp.Data
{
	public interface IProductService
	{
		public List<CommentModel> getProducts();

		public List<CommentModel> getComemntsByProductName(string productName);

		public Task createComment(string productName, string comment);		
	}
}
