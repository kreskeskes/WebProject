using AutoFixture;
using FluentAssertions;
using Moq;
using ProductService.DTO;
using ProductService.Enums;
using ProductService.RepositoryContracts;
using ProductService.ServiceContracts.IProducts;
using ProductService.Services.Products;
using System.Linq.Expressions;


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
            ProductAddRequest productAddRequest = _fixture.Build<ProductAddRequest>().With(x => x.Name, null as string).Create();

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

            ProductAddRequest productAddRequest = _fixture.Build<ProductAddRequest>().Create();


            Product product = productAddRequest.ToProduct();

            ProductResponse productResponse_expected = product.ToProductResponse();

            _productsRepositoryMock.Setup(temp => temp.AddProduct(It.IsAny<Product>())).ReturnsAsync(product);

            ProductResponse productResponse = await _productsAdderService.AddProduct(productAddRequest);

            productResponse_expected.Id = productResponse.Id;



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
            //create product that would be added to our mocked repository
            Product product = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();

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
            Product product1 = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();
            Product product2 = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();
            Product product3 = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();



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
            Product product = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();

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
            Product product1 = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();
            Product product2 = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();

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
            

            Product product1 = _fixture.Build<Product>().With(temp => temp.Name, "Pill n1" as string).Without(x => x.Categories).Without(x => x.ProductType).Create();

            Product product2 = _fixture.Build<Product>().With(temp => temp.Name, "Pill n2" as string).Without(x => x.Categories).Without(x => x.ProductType).Create();

            Product product3 = _fixture.Build<Product>().With(temp => temp.Name, "n3" as string).Without(x => x.Categories).Without(x => x.ProductType).Create();


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
            ProductUpdateRequest productUpdateRequest = _fixture.Build<ProductUpdateRequest>().With(temp => temp.Id, Guid.Empty).Create();

            Func<Task> action = async () =>
            {
                await _productsUpdaterService.UpdateProduct(productUpdateRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateProduct_ValidUpdateDetails_ToBeSuccessful()
        {
            Product product = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();

            ProductResponse productResponse = product.ToProductResponse();

            ProductUpdateRequest productUpdateRequest = productResponse.ToProductUpdateRequest();

            productUpdateRequest.Name = "changed Name";

            Product productAfterAssigningNewValues = productUpdateRequest.ToProduct();
            ProductResponse productResposeAfterAssigningNewValues_expected = productAfterAssigningNewValues.ToProductResponse();


            //Mock
            _productsRepositoryMock.Setup(temp => temp.UpdateProduct(It.IsAny<Product>())).ReturnsAsync(productAfterAssigningNewValues);

            // Mock getByProductId because it will be used within the implementation of our Service
            _productsRepositoryMock.Setup(temp => temp.GetProductByProductId(It.IsAny<Guid>())).ReturnsAsync(productAfterAssigningNewValues);


            ProductResponse productResponse_actual = await _productsUpdaterService.UpdateProduct(productUpdateRequest);

            productResposeAfterAssigningNewValues_expected.Should().Be(productResponse_actual);
        }

        #endregion

        #region SortProduct

        [Fact]
        public async Task SortProduct_ValidOutput_ToReturnSortedProducts()
        {
            Product product1 = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();

            Product product2 = _fixture.Build<Product>().Without(x => x.Categories).Without(x => x.ProductType).Create();

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