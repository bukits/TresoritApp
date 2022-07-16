using Microsoft.AspNetCore.Mvc;
using TresoritApp.Data;
using TresoritApp.Models;

namespace TresoritApp.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductController : ControllerBase
	{
		private ProductService ProductService;
		public ProductController(ProductService productService)
		{
			ProductService = productService;
		}

		[HttpGet]
		public IEnumerable<ProductModel> GetProducts()
		{
			return ProductService.getPartitions();
		}

		[HttpGet("productName")]
		public IEnumerable<ProductModel> getComments(string productName)
		{
			return ProductService.getComemntsByProductName(productName);
		}

		[HttpPost("productName")]
		public async Task createComment(string productName, string comment)
		{
			await ProductService.createComment(productName, comment);
		}
	}
}
