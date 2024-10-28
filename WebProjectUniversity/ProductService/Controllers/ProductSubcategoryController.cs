using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.ServiceContracts.IProductsProductTypes;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypesAdderService _ProductTypesAdderService;
        private readonly IProductTypesDeleterService _ProductTypesDeleterService;
        private readonly IProductTypesGetterService _ProductTypesGetterService;
        private readonly IProductTypesUpdaterService _ProductTypesUpdaterService;

        public ProductTypeController(IProductTypesUpdaterService ProductTypesUpdaterService,
            IProductTypesDeleterService ProductTypesDeleterService,
            IProductTypesAdderService ProductTypesAdderService, IProductTypesGetterService ProductTypesGetterService)
        {
            _ProductTypesAdderService = ProductTypesAdderService;
            _ProductTypesGetterService = ProductTypesGetterService;
            _ProductTypesDeleterService = ProductTypesDeleterService;
            _ProductTypesUpdaterService = ProductTypesUpdaterService;
        }
        // GET: ProductTypeController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTypeResponse>>> Get()
        {
            var ProductTypes = await _ProductTypesGetterService.GetAllProductTypes();
            return ProductTypes;
        }

        // GET: ProductTypeController/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductTypeById(Guid id)
        {
            var ProductType = await _ProductTypesGetterService.GetProductTypeResponseById(id);
            return Ok(ProductType);
        }

        // GET: ProductTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductType(ProductTypeAddRequest productTypeAddRequest)
        {
            var addedProductType = await _ProductTypesAdderService.AddProductType(productTypeAddRequest);
            return Ok(addedProductType);
        }


        // POST: ProductTypeController/Edit/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductType(ProductTypeUpdateRequest productTypeUpdateRequest)
        {
            var updatedProductType = _ProductTypesUpdaterService.UpdateProductType(productTypeUpdateRequest);
            if(updatedProductType==null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isDeleted = await _ProductTypesDeleterService.DeleteProductType(id);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
