using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductService.DTO;
using ProductService.Enums;
using System.ComponentModel;
using System.Drawing;
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
            List<AgeGenderGroup> ageGenderGroups = Enum.GetValues(typeof(AgeGenderGroup)).Cast<AgeGenderGroup>().ToList();

            ViewBag.AgeGenderGroups = ageGenderGroups.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.ToString(),
                    Value = x.ToString(),
                    Selected = false
                };
            });

            ViewBag.SizeOptions = Enum.GetValues(typeof(SizeOptions)).Cast<SizeOptions>().ToList();
            List<ProductCategoryResponse> productCategories = await _productServiceClient.GetAllProductCategoriesAsync();


            ViewBag.ProductCategoriesList = productCategories.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.Name.ToString(),
                    Value = x.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.ColorList = new List<SelectListItem>
    {
        new SelectListItem { Value = "Red", Text = "Red" },
        new SelectListItem { Value = "Blue", Text = "Blue" },
        new SelectListItem { Value = "Green", Text = "Green" },
        new SelectListItem { Value = "Yellow", Text = "Yellow" },
        new SelectListItem { Value = "Black", Text = "Black" },
        new SelectListItem { Value = "White", Text = "White" }
    };


            return View();
        }

        [HttpGet("GetProductTypes")]
        public async Task<JsonResult> GetProductTypes(Guid categoryId)
        {
            List<ProductTypeResponse> productTypes = await _productServiceClient.GetProductTypesByCategoryId(categoryId);
            var productTypeList = productTypes.Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.Name
            }).ToList();
            return Json(productTypeList);
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