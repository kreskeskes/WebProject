using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.ServiceContracts.IProductsCategories;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ICategoriesAdderService _categoriesAdderService;
        private readonly ICategoriesGetterService _categoriesGetterService;
        private readonly ICategoriesUpdaterService _categoriesUpdaterService;
        private readonly ICategoriesDeleterService _categoriesDeleterService;

        public ProductCategoryController(ICategoriesAdderService categoriesAdderService, ICategoriesDeleterService categoriesDeleterService, ICategoriesGetterService categoriesGetterService, ICategoriesUpdaterService categoriesUpdaterService)
        {
            _categoriesAdderService = categoriesAdderService;
            _categoriesGetterService = categoriesGetterService;
            _categoriesUpdaterService = categoriesUpdaterService;
            _categoriesGetterService = categoriesGetterService;
        }

        [HttpGet]
        // GET: ProductCategoryController
        public async Task<ActionResult<IEnumerable<ProductCategoryResponse>>> Index()
        {
            var result = _categoriesGetterService.GetAllProductCategories();
            return await result;
        }
        [HttpGet("{id}")]
        // GET: ProductCategoryController/Details/5
        public async Task<ActionResult<ProductCategoryResponse>> GetProductCategoryById(Guid id)
        {
            var result = _categoriesGetterService.GetProductCategoryById(id);
            return await result;
        }

        // POST: ProductCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductCategory(ProductCategoryAddRequest productCategoryAddRequest)
        {
            var createdCategory = _categoriesAdderService.AddProductCategory(productCategoryAddRequest);
            return CreatedAtAction(nameof(GetProductCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        // POST: ProductCategoryController/Edit/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<ProductCategoryResponse>> UpdateProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest)
        {
            var updatedProduct = await _categoriesUpdaterService.UpdateProductCategory(productCategoryUpdateRequest);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: ProductCategoryController/Delete/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<bool>> DeleteProductCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            bool isDeleted = await _categoriesDeleterService.DeleteProductCategory(id);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return NoContent();

        }
    }
}
