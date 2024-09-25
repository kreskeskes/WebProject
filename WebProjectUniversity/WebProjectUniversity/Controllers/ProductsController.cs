using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProjectUniversity.Core.Domain.Enums;
using WebProjectUniversity.Core.DTO;
using WebProjectUniversity.Core.ServiceContracts.ICategories;
using WebProjectUniversity.Core.ServiceContracts.IProducts;

namespace WebProjectUniversity.UI.Controllers
{

    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsAdderService _productsAdderService;
        private readonly IProductsGetterService _productsGetterService;
        private readonly IProductsDeleterService _productsDeleterService;
        private readonly IProductsUpdaterService _productsUpdaterService;
        private readonly IProductsSorterService _productsSorterService;



        public ProductsController(IProductsAdderService productsAdderService, IProductsGetterService productsGetterService, IProductsSorterService productsSorterService, IProductsUpdaterService productsUpdaterService, IProductsDeleterService productsDeleterService)
        {
            _productsAdderService = productsAdderService;
            _productsGetterService = productsGetterService;
            _productsDeleterService = productsDeleterService;
            _productsUpdaterService = productsUpdaterService;
            _productsSorterService = productsSorterService;
        }


        [Route("[action]")]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "All products";
            List<ProductResponse>? productResponses = await _productsGetterService.GetAllProducts();

            return View(productResponses);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.Title = "Add product";

            ViewBag.AgeGenderGroups= new SelectList(Enum.GetValues(typeof(AgeGenderGroup)));
            return View();
        }
    }
}
