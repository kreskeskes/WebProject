namespace ProductService.Entities
{
    public class ProductTypeProductCategory
    {
        public Guid ProductCategoryId { get; set; }
        public Guid ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
