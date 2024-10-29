using AutoFixture;
using FluentAssertions;
using Moq;
using ProductService.DTO;
using ProductService.RepositoryContracts;
using ProductService.ServiceContracts.IProductsProductTypes;
using ProductService.Services.ProductsCategories.ProductTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebProjectUniversity.ServiceTests
{
    public class ProductTypesServiceTest
    {
        private readonly IProductTypesAdderService _ProductTypesAdderService;
        private readonly IProductTypesDeleterService _ProductTypesDeleterService;
        private readonly IProductTypesGetterService _ProductTypesGetterService;
        private readonly IProductTypesUpdaterService _ProductTypesUpdaterService;
        private readonly Mock<ICategoriesRepository> _categoriesRepositoryMock;

        private readonly IProductTypesRepository _productTypesRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly Mock<IProductTypesRepository> _ProductTypesRepositoryMock;
        private readonly IFixture _fixture;

        public ProductTypesServiceTest()
        {
            _fixture = new Fixture();

            _ProductTypesRepositoryMock = new Mock<IProductTypesRepository>();
            _categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _productTypesRepository = _ProductTypesRepositoryMock.Object;
            _productsRepository= new Mock<IProductsRepository>().Object;
            _ProductTypesAdderService = new ProductTypesAdderService(_productTypesRepository, _categoriesRepositoryMock.Object);
            _ProductTypesDeleterService = new ProductTypesDeleterService(_productTypesRepository);
            _ProductTypesGetterService = new ProductTypesGetterService(_categoriesRepositoryMock.Object, _productTypesRepository);
            _ProductTypesUpdaterService = new ProductTypesUpdaterService(_productTypesRepository, _productsRepository);
        }

        #region AddProductType
        [Fact]
        public async void AddProductType_ValidInputs_ShouldAddProductTypeSuccessfully()
        {
            ProductTypeAddRequest productTypeAddRequest = _fixture.Build<ProductTypeAddRequest>().Without(x => x.ProductIds).Create();
            ProductType productType = productTypeAddRequest.ToProductType();

            _ProductTypesRepositoryMock.Setup(x => x.AddProductType(It.IsAny<ProductType>())).ReturnsAsync(productType);

            ProductTypeResponse productTypeResponse_expected = productType.ToProductTypeResponse();


            //Act
            ProductTypeResponse productTypeResponse_actual = await _ProductTypesAdderService.AddProductType(productTypeAddRequest);
            productTypeResponse_expected.Id = productTypeResponse_actual.Id;

            //Assert
            productTypeResponse_actual.Should().Be(productTypeResponse_expected);

        }

        [Fact]
        public async void AddProductType_NullProductTypeObject_ThrowsArgumentNullExpection()
        {
            ProductTypeAddRequest productTypeAddRequest = null;

            _categoriesRepositoryMock.Setup(x => x.GetProductCategoryById(It.IsAny<Guid>())).ReturnsAsync(null as ProductCategory);
            _ProductTypesRepositoryMock.Setup(x => x.AddProductType(It.IsAny<ProductType>())).ReturnsAsync(null as ProductType);


            Func<Task> action = async () =>
            {
                await _ProductTypesAdderService.AddProductType(productTypeAddRequest);
            };
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        #endregion

        #region GetAllProductTypes


        [Fact]
        public async Task GetAllProductTypes_ValidOutput_ReturnsAllProductTypes()
        {
           

            ProductType productType1 = _fixture.Build<ProductType>().Without(x => x.Products).Create();
            ProductType productType2 = _fixture.Build<ProductType>().Without(x => x.Products).Create();

            List<ProductType> productTypes = [productType1, productType2];

            _ProductTypesRepositoryMock.Setup(x => x.GetAllProductTypes()).ReturnsAsync(productTypes);

            List<ProductTypeResponse> productTypes_expected =
                productTypes.Select(x => x.ToProductTypeResponse()).ToList();

            List<ProductTypeResponse> productTypes_actual =
                await _ProductTypesGetterService.GetAllProductTypes();

            productTypes_actual.Should().BeEquivalentTo(productTypes_expected);
        }


        [Fact]
        public async Task GetAllProductTypes_NoProductTypesExist_ReturnsAnEmptyList()
        {
            _ProductTypesRepositoryMock.Setup(x => x.GetAllProductTypes()).ReturnsAsync(new List<ProductType>());

            List<ProductTypeResponse> productTypes = await _ProductTypesGetterService.GetAllProductTypes();

            productTypes.Should().BeEmpty();
        }
        #endregion

        #region GetProductTypeById

        [Fact]
        public async void GetProductTypesById_ValidGuid_ReturnsProductType()
        {
            ProductType ProductType = _fixture.Build<ProductType>().Without(x => x.Products).Create();

            _ProductTypesRepositoryMock.Setup(x => x.GetProductTypeById(It.IsAny<Guid>())).ReturnsAsync(ProductType);

            ProductTypeResponse productType_expected = ProductType.ToProductTypeResponse();

            ProductTypeResponse productType_actual = await _ProductTypesGetterService
                .GetProductTypeResponseById(ProductType.Id);

            productType_actual.Should().Be(productType_expected);
        }

        [Fact]
        public async void GetProductTypesById_InvalidGuid_ReturnsArgumentNullException()
        {
            Guid productTypeId = Guid.Empty;

            _ProductTypesRepositoryMock.Setup(x => x.GetProductTypeById(It.IsAny<Guid>())).ReturnsAsync(null as ProductType);

            Func<Task> action = async () =>
            {
                await _ProductTypesGetterService.GetProductTypeResponseById(productTypeId);
            };
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
        #endregion

        #region DeleteProductType

        [Fact]
        public async void DeleteProductType_ValidGuid_ProductTypeRemovedSuccessfully_ReturnsTrue()
        {
            ProductType ProductType = _fixture.Build<ProductType>().Without(x => x.Products).Create();

            _ProductTypesRepositoryMock.Setup(x => x.DeleteProductTypeById(ProductType.Id)).ReturnsAsync(true);

            _ProductTypesRepositoryMock.Setup(x => x.GetProductTypeById(It.IsAny<Guid>())).ReturnsAsync(ProductType);

            bool isDeleted_actual = await _ProductTypesDeleterService.DeleteProductType(ProductType.Id);

            isDeleted_actual.Should().BeTrue();
        }


        [Fact]
        public async void DeleteProductType_ProductTypeNotFound_ProductTypeNotRemoved_ReturnsFalse()
        {
            Guid productTypeId = Guid.NewGuid();

            bool isDeleted_actual = await _ProductTypesDeleterService.DeleteProductType(productTypeId);

            isDeleted_actual.Should().BeFalse();
        }

        [Fact]
        public async void DeleteProductType_NullId_ProductTypeNotRemoved_ThrowsArgumentNullException()
        {
            Guid productTypeId = Guid.Empty;

            Func<Task> action = async () =>
            {
                await _ProductTypesDeleterService.DeleteProductType(productTypeId);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        #endregion

        #region UpdateProductType

        [Fact]
        public async void UpdateProductType_ValidProductTypeObject_UpdateSuccessful_ReturnsUpdatedObject()
        {
            ProductTypeUpdateRequest productTypeUpdateRequest = _fixture.Build<ProductTypeUpdateRequest>()
                .Without(x => x.ProductIds).Create();

            ProductType updatedProductType_expected = productTypeUpdateRequest.ToProductType();

            _ProductTypesRepositoryMock.Setup(x => x.GetProductTypeById(It.IsAny<Guid>())).ReturnsAsync(updatedProductType_expected);
            _ProductTypesRepositoryMock.Setup(x => x.UpdateProductType(It.IsAny<ProductType>())).ReturnsAsync(updatedProductType_expected);

            ProductTypeResponse productTypeResponse_actual = await _ProductTypesUpdaterService.UpdateProductType(productTypeUpdateRequest);
            ProductTypeResponse productTypeResponse_expected = updatedProductType_expected.ToProductTypeResponse();

            productTypeResponse_actual.Should().Be(productTypeResponse_expected);
        }

        [Fact]
        public async void UpdateProductType_NullId_UpdateFailed_ThrowsArgumentExpection()
        {
            ProductTypeUpdateRequest productTypeUpdateRequest = _fixture.Build<ProductTypeUpdateRequest>()
     .Without(x => x.ProductIds).With(x => x.Id, Guid.Empty).Create();

            Func<Task> action = async () =>
            {
                await _ProductTypesUpdaterService.UpdateProductType(productTypeUpdateRequest);
            };

            action.Should().ThrowAsync<ArgumentException>();
        }
        [Fact]
        public async void UpdateProductType_NullUpdateObject_UpdateFailed_ThrowsArgumentNullExpection()
        {
            ProductTypeUpdateRequest productTypeUpdateRequest = null;

            Func<Task> action = async () =>
            {
                await _ProductTypesUpdaterService.UpdateProductType(productTypeUpdateRequest);
            };

            action.Should().ThrowAsync<ArgumentNullException>();
        }
        #endregion



    }
}
