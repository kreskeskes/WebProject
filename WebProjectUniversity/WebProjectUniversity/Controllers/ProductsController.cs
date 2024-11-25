using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductService.DTO;
using ProductService.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using WebProjectUniversity.UI.Clients;
using WebProjectUniversity.UI.Helpers;


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
            // Convert enum to a list of SelectListItem
            var ageGenderGroups = Enum.GetValues(typeof(AgeGenderGroup))
                .Cast<AgeGenderGroup>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(), // The value that will be sent to the server
                    Text = e.GetAttribute<DisplayAttribute>()?.Name ?? e.ToString() // Display name
                })
                .ToList();
            ViewBag.AgeGenderGroups = ageGenderGroups; // Assign to ViewBag

            ViewBag.BrandsList = new List<string> { "H&M", "Bershka", "Zara" }; // Example brands
            ViewBag.StylesList = new List<string> { "Casual", "Formal", "Sport", "Vintage" }; // Example styles
            ViewBag.LengthsList = new List<string> { "Short", "Medium", "Long" }; // Example lengths


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
        public async Task<JsonResult> GetProductTypes(List<Guid> categoryIds)
        {
            List<ProductTypeResponse> productTypes = new List<ProductTypeResponse>();
            foreach (Guid categoryId in categoryIds)
            {
                productTypes.AddRange(await _productServiceClient.GetProductTypesByCategoryId(categoryId));
            }
            var productTypeList = productTypes.Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.Name.ToString()
            }).ToList();

            return Json(productTypeList);
        }

        [HttpPost("AddProduct")]
        [ValidateAntiForgeryToken] // CSRF protection
        public async Task<IActionResult> AddProduct(ProductAddRequest productAddRequest)

        {
            // Convert enum to a list of SelectListItem
            var ageGenderGroups = Enum.GetValues(typeof(AgeGenderGroup))
                .Cast<AgeGenderGroup>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(), // The value that will be sent to the server
                    Text = e.GetAttribute<DisplayAttribute>()?.Name ?? e.ToString() // Display name
                })
                .ToList();



            ViewBag.BrandsList = new List<string> { "H&M", "Bershka", "Zara" }; // Example brands
            ViewBag.StylesList = new List<string> { "Casual", "Formal", "Sport", "Vintage" }; // Example styles
            ViewBag.LengthsList = new List<string> { "Short", "Medium", "Long" }; // Example lengths

            ViewBag.AgeGenderGroups = ageGenderGroups; // Assign to ViewBag


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
            List<ProductTypeResponse> allProductTypes = await _productServiceClient.GetAllProductTypesAsync();
            ViewBag.ProductTypesList = allProductTypes.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name.ToString(),
                    Selected = false
                };
            }
            );


            if (ModelState.IsValid)
            {
                await _productServiceClient.AddProductAsync(productAddRequest);

                return RedirectToAction("Index");
            }



            return View(productAddRequest);
        }
    }

    // Other actions like Create, Edit, Delete would follow a similar pattern
}
