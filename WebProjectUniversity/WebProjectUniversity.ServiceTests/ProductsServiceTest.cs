using AutoFixture;
using FluentAssertions;
using Moq;
using ServiceContracts.Enums;
using System.Linq.Expressions;
using WebProjectUniversity.Core.Domain.Entities;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Core.DTO;
using WebProjectUniversity.Core.Service;
using WebProjectUniversity.Core.Service.Products;
using WebProjectUniversity.Core.ServiceContracts.ICategories;
using WebProjectUniversity.Core.ServiceContracts.IProducts;

namespace WebProjectUniversity.ServiceTests
{
    public class ProductsServiceTest
    {
        private readonly IProductsAdderService _productsAdderService;
        private readonly IProductsGetterService _productsGetterService;
        private readonly IProductsDeleterService _productsDeleterService;
        private readonly IProductsSorterService _productsSorterService;
        private readonly IProductsUpdaterService _productsUpdaterService;


        private readonly IProductsRepository _productsRepository;
        private readonly Mock<IProductsRepository> _productsRepositoryMock;
        private readonly IFixture _fixture;

        public ProductsServiceTest()
        {
            _fixture = new Fixture();

            _productsRepositoryMock = new Mock<IProductsRepository>();
            _productsRepository = _productsRepositoryMock.Object;

            _productsAdderService = new ProductsAdderService(_productsRepository);
            _productsDeleterService = new ProductsDeleterService(_productsRepository);
            _productsGetterService = new ProductsGetterService(_productsRepository);
            _productsSorterService = new ProductsSorterService(_productsRepository);
            _productsUpdaterService = new ProductsUpdaterService(_productsRepository);

        }




        #region AddProduct
        [Fact]
        public async Task AddProduct_NullProduct_ToBeArgumentNullException()
        {
            //Arrange
            ProductAddRequest productAddRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _productsAdderService.AddProduct(productAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();

        }

        [Fact]

        public async Task AddProduct_ProductName_ToBeNull()
        {
            //Arrange
            ProductCategory productCategory = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory category = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory).With(x => x.CategoryId, productCategory.Id).Create();

            List<ProductProductCategory> productProductCategories = new List<ProductProductCategory>() { category };

            ProductAddRequest productAddRequest = _fixture.Build<ProductAddRequest>().With(temp => temp.Name, null as string).With(x => x.ProductCategories, productProductCategories).Create();

            Product product = productAddRequest.ToProduct();

            _productsRepositoryMock.Setup(temp => temp.AddProduct(It.IsAny<Product>())).ReturnsAsync(product);


            //Act
            Func<Task> action = async () =>
            {
                await _productsAdderService.AddProduct(productAddRequest);
            };


            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task AddProduct_ValidProductToBeAdded()
        {
            //Arrange
            ProductCategory productCategory = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory category = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory).With(x => x.CategoryId, productCategory.Id).Create();

            List<ProductProductCategory> productProductCategories = new List<ProductProductCategory>() { category };

            ProductAddRequest productAddRequest = _fixture.Build<ProductAddRequest>().With(x => x.ProductCategories, productProductCategories).Create();


            Product product = productAddRequest.ToProduct();

            ProductResponse productResponse_expected = product.ToProductResponse();

            _productsRepositoryMock.Setup(temp => temp.AddProduct(It.IsAny<Product>())).ReturnsAsync(product);

            ProductResponse productResponse = await _productsAdderService.AddProduct(productAddRequest);

            productResponse_expected.Id = productResponse.Id;

            //Act
            Func<Task> action = async () =>
            {
                await _productsAdderService.AddProduct(productAddRequest);
            };


            //Assert
            productResponse.Should().Be(productResponse_expected);
        }
        #endregion

        #region  DeleteProduct
        [Fact]
        public async Task DeleteProduct_NullId()
        {
            Guid? productId = null;

            Func<Task> action = async () =>
            {
                await _productsDeleterService.DeleteProduct(productId);
            };
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteProduct_ProductNotFound()
        {
            Guid? productId = Guid.NewGuid();

            bool resultOfDeletion = await _productsDeleterService.DeleteProduct(productId);

            resultOfDeletion.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteProduct_ProductDeleted()
        {
            ProductCategory productCategory1 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductCategory productCategory2 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory productProductcategory1 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory1).With(x => x.CategoryId, productCategory1.Id).Create();

            ProductProductCategory productProductcategory2 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory2).With(x => x.CategoryId, productCategory2.Id).Create();
            List<ProductProductCategory> productProductCategories1 = new List<ProductProductCategory>() { productProductcategory1 };
            List<ProductProductCategory> productProductCategories2 = new List<ProductProductCategory>() { productProductcategory1, productProductcategory2 };

            //create product that would be added to our mocked repository
            Product product = _fixture.Build<Product>().Without(x => x.ProductSubcategory)
    .With(p => p.ProductCategories, productProductCategories1).Create();

            ProductResponse productResponse = product.ToProductResponse();

            // we mock it up because our method will internally use this method
            _productsRepositoryMock.Setup(temp => temp.GetProductByProductId(It.IsAny<Guid>())).ReturnsAsync(product);


            _productsRepositoryMock.Setup(temp => temp.DeleteProductByProductId(It.IsAny<Guid>())).ReturnsAsync(true);

            bool resultOfDeletion = await _productsDeleterService.DeleteProduct(product.Id);
            resultOfDeletion.Should().BeTrue();
        }
        #endregion

        #region GetAllProducts
        [Fact]
        public async Task GetAllProducts_ReturnsEmptyList()
        {

            _productsRepositoryMock.Setup(temp => temp.GetAllProducts()).ReturnsAsync(new List<Product>());

            List<ProductResponse>? products = await _productsGetterService.GetAllProducts();

            products.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllProducts_ReturnsAFewProducts()
        {
            ProductCategory productCategory1 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductCategory productCategory2 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory productProductcategory1 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory1).With(x => x.CategoryId, productCategory1.Id).Create();

            ProductProductCategory productProductcategory2 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory2).With(x => x.CategoryId, productCategory2.Id).Create();
            List<ProductProductCategory> productProductCategories1 = new List<ProductProductCategory>() { productProductcategory1 };
            List<ProductProductCategory> productProductCategories2 = new List<ProductProductCategory>() { productProductcategory1, productProductcategory2 };

            Product product1 = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories1).Without(x => x.ProductSubcategory).Create();
            Product product2 = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories2).Without(x => x.ProductSubcategory).Create();
            Product product3 = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories2).Without(x => x.ProductSubcategory).Create();



            List<Product> products = [product1, product2, product3];

            _productsRepositoryMock.Setup(temp => temp.GetAllProducts()).ReturnsAsync(products);

            List<ProductResponse> productResponses_expected = products.Select(temp => temp.ToProductResponse()).ToList();

            List<ProductResponse>? productResponses_actual = await _productsGetterService.GetAllProducts();

            productResponses_actual.Should().BeEquivalentTo(productResponses_expected);


        }
        #endregion

        #region GetProductById

        [Fact]
        public async Task GetProductById_NullId()
        {
            Guid id = Guid.Empty;

            ProductResponse? productResponse = await _productsGetterService.GetProductByProductId(id);

            productResponse.Should().BeNull();
        }

        [Fact]
        public async Task GetProductById_ValidIdReturnsProduct()
        {
            ProductCategory productCategory1 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductCategory productCategory2 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory productProductcategory1 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory1).With(x => x.CategoryId, productCategory1.Id).Create();

            ProductProductCategory productProductcategory2 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory2).With(x => x.CategoryId, productCategory2.Id).Create();
            List<ProductProductCategory> productProductCategories1 = new List<ProductProductCategory>() { productProductcategory1 };
            List<ProductProductCategory> productProductCategories2 = new List<ProductProductCategory>() { productProductcategory1, productProductcategory2 };

            Product product = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories2)
    .Without(p => p.ProductSubcategory).Create();

            ProductResponse productResponse_expected = product.ToProductResponse();

            _productsRepositoryMock.Setup(temp => temp.GetProductByProductId(It.IsAny<Guid>())).ReturnsAsync(product);

            ProductResponse productResponse_actual = await _productsGetterService.GetProductByProductId(product.Id);

            productResponse_expected.Should().Be(productResponse_actual);
        }

        #endregion

        #region GetFilteredProducts

        [Fact]
        public async Task GetFilteredProducts_SearchByNull_SearchStringNull_ReturnsLikeGetAllProducts()
        {
            ProductCategory productCategory1 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductCategory productCategory2 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory productProductcategory1 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory1).With(x => x.CategoryId, productCategory1.Id).Create();

            ProductProductCategory productProductcategory2 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory2).With(x => x.CategoryId, productCategory2.Id).Create();
            List<ProductProductCategory> productProductCategories1 = new List<ProductProductCategory>() { productProductcategory1 };
            List<ProductProductCategory> productProductCategories2 = new List<ProductProductCategory>() { productProductcategory1, productProductcategory2 };

            Product product1 = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories2)
    .Without(p => p.ProductSubcategory).Create();
            Product product2 = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories1)
    .Without(p => p.ProductSubcategory).Create();

            List<Product> products = new List<Product>() { product1, product2 };

            List<ProductResponse> productResponses = products.Select(temp => temp.ToProductResponse()).ToList();



            _productsRepositoryMock.Setup(temp => temp.GetFilteredProducts(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(products);

            string? searchString = null;

            List<ProductResponse> productResponses_expected = productResponses;

            List<ProductResponse> productResponses_actual = await _productsGetterService.GetFilteredProducts(nameof(Product.Name), searchString);

            productResponses_actual.Should().BeEquivalentTo(productResponses_expected);

        }


        [Fact]
        public async Task GetFilteredProducts_SearchByProductName_SearchStringValid_ToBeSuccessful()
        {
            ProductCategory productCategory1 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductCategory productCategory2 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory productProductcategory1 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory1).With(x => x.CategoryId, productCategory1.Id).Create();

            ProductProductCategory productProductcategory2 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory2).With(x => x.CategoryId, productCategory2.Id).Create();

            List<ProductProductCategory> productProductCategories1 = new List<ProductProductCategory>() { productProductcategory1 };
            List<ProductProductCategory> productProductCategories2 = new List<ProductProductCategory>() { productProductcategory1, productProductcategory2 };

            Product product1 = _fixture.Build<Product>().With(x => x.ProductCategories, productProductCategories2).With(temp => temp.Name, "Pill n1" as string).Without(p => p.ProductSubcategory).Create();

            Product product2 = _fixture.Build<Product>().With(temp => temp.Name, "Pill n2" as string).With(x => x.ProductCategories, productProductCategories1).Without(p => p.ProductSubcategory).Create();

            Product product3 = _fixture.Build<Product>().With(temp => temp.Name, "n3" as string).With(x => x.ProductCategories, productProductCategories2).Without(p => p.ProductSubcategory).Create();


            List<Product> products = new List<Product>() { product1, product2, product3 };
            List<Product> productsFiltered = products.Where(temp => temp.Name.Contains("Pill")).ToList();

            List<ProductResponse> productResponses = products.Select(temp => temp.ToProductResponse()).ToList();

            _productsRepositoryMock.Setup(temp => temp.GetFilteredProducts(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(productsFiltered);



            List<ProductResponse> productResponses_expected = productResponses.Where(temp => temp.Name.Contains("Pill")).ToList();


            List<ProductResponse> productResponses_actual = await _productsGetterService.GetFilteredProducts(nameof(Product.Name), "Pill");

            productResponses_actual.Should().BeEquivalentTo(productResponses_expected);

        }

        #endregion

        #region UpdateProduct

        [Fact]
        public async Task UpdateProduct_NullProductUpdateDetails_ToBeArgumentNullException()
        {

            ProductUpdateRequest productUpdateRequest = null;

            Func<Task> action = async () =>
            {
                await _productsUpdaterService.UpdateProduct(productUpdateRequest);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateProduct_InvalidProductId_ToBeArgumentException()
        {
            ProductCategory productCategory1 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductCategory productCategory2 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory productProductcategory1 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory1).With(x => x.CategoryId, productCategory1.Id).Create();

            ProductProductCategory productProductcategory2 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory2).With(x => x.CategoryId, productCategory2.Id).Create();

            List<ProductProductCategory> productProductCategories1 = new List<ProductProductCategory>() { productProductcategory1 };
            List<ProductProductCategory> productProductCategories2 = new List<ProductProductCategory>() { productProductcategory1, productProductcategory2 };

            ProductUpdateRequest productUpdateRequest = _fixture.Build<ProductUpdateRequest>().With(temp => temp.Id, Guid.Empty).Without(x=>x.ProductSubcategoryId).With(x=>x.ProductCategories, productProductCategories2).Create();

            Func<Task> action = async () =>
            {
                await _productsUpdaterService.UpdateProduct(productUpdateRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateProduct_ValidUpdateDetault_ToBeSuccessful()
        {
            ProductCategory productCategory1 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductCategory productCategory2 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory productProductcategory1 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory1).With(x => x.CategoryId, productCategory1.Id).Create();

            ProductProductCategory productProductcategory2 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory2).With(x => x.CategoryId, productCategory2.Id).Create();

            List<ProductProductCategory> productProductCategories1 = new List<ProductProductCategory>() { productProductcategory1 };
            List<ProductProductCategory> productProductCategories2 = new List<ProductProductCategory>() { productProductcategory1, productProductcategory2 };


            Product product = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories2)
    .Without(p => p.ProductSubcategory).Create();

            ProductResponse productResponse = product.ToProductResponse();

            ProductUpdateRequest productUpdateRequest = productResponse.ToProductUpdateRequest();

            productUpdateRequest.Name = "changed Name";

            Product productAfterAssigningNewValues = productUpdateRequest.ToProduct();
            ProductResponse prodcutResposeAfterAssigningNewValues_expected = productAfterAssigningNewValues.ToProductResponse();


            //Mock
            _productsRepositoryMock.Setup(temp => temp.UpdateProduct(It.IsAny<Product>())).ReturnsAsync(productAfterAssigningNewValues);

            // Mock getByProductId because it will be used within the implementation of our Service
            _productsRepositoryMock.Setup(temp => temp.GetProductByProductId(It.IsAny<Guid>())).ReturnsAsync(productAfterAssigningNewValues);


            ProductResponse productResponse_actual = await _productsUpdaterService.UpdateProduct(productUpdateRequest);

            prodcutResposeAfterAssigningNewValues_expected.Should().Be(productResponse_actual);
        }

        #endregion

        #region SortProduct

        [Fact]
        public async Task SortProduct_ValidOutput_ToReturnSortedProducts()
        {
            ProductCategory productCategory1 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductCategory productCategory2 = _fixture.Build<ProductCategory>().Without(x => x.Products).Without(x => x.ProductSubcategories).Create();

            ProductProductCategory productProductcategory1 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory1).With(x => x.CategoryId, productCategory1.Id).Create();

            ProductProductCategory productProductcategory2 = _fixture.Build<ProductProductCategory>().Without(x => x.Product).Without(x => x.ProductId).With(x => x.ProductCategory, productCategory2).With(x => x.CategoryId, productCategory2.Id).Create();

            List<ProductProductCategory> productProductCategories1 = new List<ProductProductCategory>() { productProductcategory1 };
            List<ProductProductCategory> productProductCategories2 = new List<ProductProductCategory>() { productProductcategory1, productProductcategory2 };


            Product product1 = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories2)
    .Without(p => p.ProductSubcategory).Create();

            Product product2 = _fixture.Build<Product>().With(p => p.ProductCategories, productProductCategories2)
    .Without(p => p.ProductSubcategory).Create();

            List<Product> products = new List<Product>() { product1, product2 };

            List<ProductResponse> productResponses = products.Select(temp => temp.ToProductResponse()).ToList();

            //Mock it because it will be used within our service method
            _productsRepositoryMock.Setup(temp => temp.GetAllProducts()).ReturnsAsync(products);

            List<ProductResponse> productResponsesAfterSorting_expected = productResponses.OrderByDescending(temp => temp.Name).ToList();


            List<ProductResponse> productsSortedByService_actual = await _productsSorterService.GetSortedProducts(productResponses, nameof(Product.Name), SortOrderOptions.Descending);

            productsSortedByService_actual.Should().BeInDescendingOrder(temp => temp.Name);
        }
        #endregion

    }
}