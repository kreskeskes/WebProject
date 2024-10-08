using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.ServiceContracts.IProducts;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsGetterService _productsGetterService;
        private readonly IProductsAdderService _productsAdderService;
        private readonly IProductsUpdaterService _productsUpdaterService;
        private readonly IProductsDeleterService _productsDeleterService;

        public ProductController(
            IProductsGetterService productsGetterService,
            IProductsAdderService productsAdderService,
            IProductsUpdaterService productsUpdaterService,
            IProductsDeleterService productsDeleterService)
        {
            _productsGetterService = productsGetterService;
            _productsAdderService = productsAdderService;
            _productsUpdaterService = productsUpdaterService;
            _productsDeleterService = productsDeleterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> Get()
        {
            var products = _productsGetterService.GetAllProducts();
            return await products;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = _productsGetterService.GetProductByProductId(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddRequest productAddRequest)
        {
            var createdProduct = _productsAdderService.AddProduct(productAddRequest);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            var updatedProduct = _productsUpdaterService.UpdateProduct(productUpdateRequest);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            bool isDeleted = await _productsDeleterService.DeleteProduct(id);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return NoContent();

        }
    }
}
