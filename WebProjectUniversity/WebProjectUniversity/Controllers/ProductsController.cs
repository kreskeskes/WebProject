using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using WebProjectUniversity.UI.Clients;

namespace WebProjectUniversity.UI.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductServiceClient _productServiceClient;

        public ProductsController(ProductServiceClient productServiceClient)
        {
            _productServiceClient = productServiceClient;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            
            var products = await _productServiceClient.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet("AddProduct")]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductAddRequest productAddRequest)
        {
           ViewBag.AgeGenderGroups = _productServiceClient.GetAllProductCategoriesAsync();
           await _productServiceClient.AddProductAsync(productAddRequest);
            if (!ModelState.IsValid)
            {
                return View(productAddRequest);

            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // Other actions like Create, Edit, Delete would follow a similar pattern
    }
}