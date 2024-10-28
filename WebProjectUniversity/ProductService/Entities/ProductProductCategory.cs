namespace ProductService.Entities
{
    public class ProductProductCategory
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
