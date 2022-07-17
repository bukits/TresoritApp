using Microsoft.AspNetCore.Mvc;
using TresoritApp.Data;
using TresoritApp.Models;

namespace TresoritApp.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductController : ControllerBase
	{
		private IProductService _IProductService;
		public ProductController(IProductService productService)
		{
			_IProductService = productService;
		}

		[HttpGet]
		public IEnumerable<CommentModel> GetProducts()
		{
			return _IProductService.getProducts();
		}

		[HttpGet("productName")]
		public IEnumerable<CommentModel> getComments(string productName)
		{
			return _IProductService.getComemntsByProductName(productName);
		}

		[HttpPost("productName")]
		public void createComment(string productName, string comment)
		{
			_IProductService.createComment(productName, comment);
		}
	}
}
