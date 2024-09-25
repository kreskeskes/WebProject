using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Core.DTO;
using WebProjectUniversity.Core.Service.ProductsCategories;
using WebProjectUniversity.Core.ServiceContracts.IProductsCategories;

namespace WebProjectUniversity.ServiceTests
{
    public class CategoriesServiceTest
    {
        private readonly ICategoriesAdderService _categoriesAdderService;
        private readonly ICategoriesDeleterService _categoriesDeleterService;
        private readonly ICategoriesGetterService _categoriesGetterService;
        private readonly ICategoriesUpdaterService _categoriesUpdaterService;

        private readonly ICategoriesRepository _categoriesRepository;
        private readonly Mock<ICategoriesRepository> _categoriesRepositoryMock;

        private readonly IFixture _fixture;

        public CategoriesServiceTest()
        {
            _categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _categoriesRepository = _categoriesRepositoryMock.Object;
            _fixture = new Fixture();

            _categoriesAdderService = new CategoriesAdderService(_categoriesRepository);
            _categoriesDeleterService = new CategoriesDeleterService(_categoriesRepository);
            _categoriesGetterService = new CategoriesGetterService(_categoriesRepository);
            _categoriesUpdaterService = new CategoriesUpdaterService(_categoriesRepository);
        }

        #region AddProductCategory
        [Fact]
        public async Task AddProductCategory_CategoryNameToBeNull_ToThrowArgumentException()
        {
            ProductCategoryAddRequest request = _fixture.Build<ProductCategoryAddRequest>().With(temp => temp.Name, null as string).Without(temp=>temp.ProductSubcategories).Create();

            Func<Task> action = async () =>
            {
                await _categoriesAdderService.AddProductCategory(request);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task AddProductCategory_ProductCategoryAddRequestToBeNull_ToThrowArgumentNullException()
        {
            ProductCategoryAddRequest addRequest = null;

            Func<Task> action = async () =>
            {
                await _categoriesAdderService.AddProductCategory(addRequest);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddProductCategory_ValidCategoryToAdd_ToBeSuccessful()
        {
            ProductCategoryAddRequest addRequest = _fixture.Build<ProductCategoryAddRequest>().Without(temp => temp.ProductSubcategories).Create();

            ProductCategory productCategory = addRequest.ToProductCategory();
            ProductCategoryResponse productCategoryResponse_expected = productCategory.ToProductCategoryResponse();

            _categoriesRepositoryMock.Setup(x => x.AddProductCategory(It.IsAny<ProductCategory>())).ReturnsAsync(productCategory);

            ProductCategoryResponse productCategory_actual = await _categoriesAdderService.AddProductCategory(addRequest);
            productCategoryResponse_expected.Id = productCategory_actual.Id;

            productCategory_actual.Should().Be(productCategoryResponse_expected);

        }
        #endregion

        #region DeleteProductCategory

        [Fact]
        public async Task DeleteProductCategory_CategoryNotFound()
        {
            Guid id = Guid.NewGuid();

            bool result = await _categoriesDeleterService.DeleteProductCategory(id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteProductCategory_CategoryDeletedSuccessfully()
        {
            ProductCategory productCategory = _fixture.Build<ProductCategory>().Without(temp => temp.ProductSubcategories).Without(temp => temp.Products).Create();

            _categoriesRepositoryMock.Setup(x => x.DeleteProductCategoryBytId(It.IsAny<Guid>())).ReturnsAsync(true);
            _categoriesRepositoryMock.Setup(x => x.GetProductCategoryById(It.IsAny<Guid>())).ReturnsAsync(productCategory);

            bool result = await _categoriesDeleterService.DeleteProductCategory(productCategory.Id);

            result.Should().BeTrue();
        }

        #endregion

        #region GetProductCategoryById

        [Fact]
        public async Task GetProductCategoryById_NullId_ToThrowArgumentNullException()
        {
            Guid id = Guid.Empty;

            Func<Task> action = async () => await _categoriesGetterService.GetProductCategoryById(id);
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetProductCategoryByProductId_ProductNotFound()
        {
            Guid id = Guid.NewGuid();

            _categoriesRepositoryMock.Setup(x => x.GetProductCategoryById(It.IsAny<Guid>())).ReturnsAsync(null as ProductCategory);

            ProductCategoryResponse productCategoryResponse = await _categoriesGetterService.GetProductCategoryById(id);

            productCategoryResponse.Should().BeNull();
        }

        [Fact]
        public async Task GetProductCategoryByProductId_ProductCategoryFound()
        {
            ProductCategory productCategory = _fixture.Build<ProductCategory>().Without(x => x.ProductSubcategories).Without(x => x.Products).Create();
            ProductCategoryResponse productCategoryResponse_expected = productCategory.ToProductCategoryResponse();

            _categoriesRepositoryMock.Setup(x => x.GetProductCategoryById(It.IsAny<Guid>())).ReturnsAsync(productCategory);
            ProductCategoryResponse productCategoryResponse_actual = await _categoriesGetterService.GetProductCategoryById(productCategory.Id.Value);

            productCategoryResponse_actual.Should().Be(productCategoryResponse_expected);
        }
        #endregion

        #region UpdateProductCategory

        [Fact]
        public async Task UpdateProductCategory_InvalidUpdateCategoryDetails_ToThrowArgumentNullException()
        {
            ProductCategoryUpdateRequest productCategoryUpdateRequest = null;

            Func<Task> action = async () =>
            {
                await _categoriesUpdaterService.UpdateProductCategory(productCategoryUpdateRequest);
            };
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateProductCategory_Invalid_ProductCategoryID_ToBeArgumentException()
        {
            ProductCategoryUpdateRequest productCategoryUpdateRequest = _fixture.Build<ProductCategoryUpdateRequest>().With(x => x.Id, Guid.Empty).Without(x => x.ProductSubcategories).Create();

            Func<Task> action = async () =>
            {
                await _categoriesUpdaterService.UpdateProductCategory(productCategoryUpdateRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateProductCategory_Valid_ToBeSuccessful()
        {
            ProductCategoryUpdateRequest productUpdateRequest = _fixture.Build<ProductCategoryUpdateRequest>().Without(temp => temp.ProductSubcategories).Create();

            ProductCategory productCategory = productUpdateRequest.ToProductCategory();
            ProductCategoryResponse productCategoryResponse_expected = productCategory.ToProductCategoryResponse();

            _categoriesRepositoryMock.Setup(x => x.UpdateProductCategory(It.IsAny<ProductCategory>())).ReturnsAsync(productCategory);
            _categoriesRepositoryMock.Setup(x => x.GetProductCategoryById(It.IsAny<Guid>())).ReturnsAsync(productCategory);

            ProductCategoryResponse productCategoryResponse_actual = await _categoriesUpdaterService.UpdateProductCategory(productUpdateRequest);

            productCategoryResponse_actual.Should().Be(productCategoryResponse_expected);

        }
        #endregion
    }
}
