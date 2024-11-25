
using Newtonsoft.Json;
using Ocelot.Infrastructure;
using ProductService.DTO;
using System.Linq.Expressions;

namespace WebProjectUniversity.UI.Clients
{
    public class ProductServiceClient
    {
        private readonly HttpClient _httpClient;
        

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _httpClient.BaseAddress = new Uri("http://host.docker.internal:8080/");
        }

        #region Product
        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("http://host.docker.internal:8080/api/product");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Log the JSON for inspection
            Console.WriteLine(jsonString);

            response.EnsureSuccessStatusCode();

            // Deserialize using Newtonsoft.Json
            return JsonConvert.DeserializeObject<List<ProductResponse>>(jsonString);
        }

        public async Task<ProductResponse> GetProductByIdAsync(Guid id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductResponse>($"http://host.docker.internal:8080/api/product/{id}");
            return product;
        }

        public async Task<ProductResponse> AddProductAsync(ProductAddRequest product)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://host.docker.internal:8080/api/product", product);
                var jsonString = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ProductResponse>();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
            public async Task UpdateProductAsync(ProductUpdateRequest product)
            {
                var response = await _httpClient.PutAsJsonAsync($"http://host.docker.internal:8080/api/product/{product.Id}", product);
                response.EnsureSuccessStatusCode();
            }

            public async Task DeleteProductAsync(Guid id)
            {
                var response = await _httpClient.DeleteAsync($"http://host.docker.internal:8080/api/product/{id}");
                response.EnsureSuccessStatusCode();
            }

            #endregion


            #region Category
            public async Task<List<ProductCategoryResponse>> GetAllProductCategoriesAsync()
            {
                var response = await _httpClient.GetAsync("http://host.docker.internal:8080/api/productcategory");
                var jsonString = await response.Content.ReadAsStringAsync();

                // Log the JSON for inspection
                Console.WriteLine(jsonString);

                response.EnsureSuccessStatusCode();

                // Deserialize using Newtonsoft.Json
                return JsonConvert.DeserializeObject<List<ProductCategoryResponse>>(jsonString);
            }


            public async Task<ProductCategoryResponse> GetProductCategoryByCategoryId(Guid id)
            {
                var response = await _httpClient.GetFromJsonAsync<ProductCategoryResponse>($"http://host.docker.internal:8080/api/productcategory/{id}");
                return response;
            }



            public async Task<ProductCategoryResponse> AddProductCategory(ProductCategoryAddRequest productCategoryAddRequest)
            {
                var response = await _httpClient.PostAsJsonAsync("http://host.docker.internal:8080/api/productcategory/", productCategoryAddRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
            }


            public async Task DeleteProductCategory(Guid id)
            {
                var response = await _httpClient.DeleteAsync($"http://host.docker.internal:8080/api/productcategory/{id}");
                response.EnsureSuccessStatusCode();
            }

            public async Task<ProductCategoryResponse> UpdateProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest)
            {
                var response = await _httpClient.PutAsJsonAsync("http://host.docker.internal:8080/api/productcategory/", productCategoryUpdateRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
            }

            #endregion


            #region ProductType

            public async Task<List<ProductTypeResponse>> GetAllProductTypesAsync()
            {
                var response = await _httpClient.GetAsync("http://host.docker.internal:8080/api/ProductType/");
                var jsonString = await response.Content.ReadAsStringAsync();

                // Log the JSON for inspection
                Console.WriteLine(jsonString);

                response.EnsureSuccessStatusCode();

                // Deserialize using Newtonsoft.Json
                return JsonConvert.DeserializeObject<List<ProductTypeResponse>>(jsonString);
            }


            public async Task<ProductTypeResponse> GetProductTypeByProductTypeId(Guid id)
            {
                var response = await _httpClient.GetFromJsonAsync<ProductTypeResponse>($"http://host.docker.internal:8080/api/ProductType/{id}");
                return response;
            }

            public async Task<List<ProductTypeResponse>> GetProductTypesByCategoryId(Guid categoryId)
            {
                var response = await _httpClient.GetAsync($"http://host.docker.internal:8080/api/ProductType/category/productType/{categoryId}");
                var jsonString = await response.Content.ReadAsStringAsync();

                // Log the JSON for inspection
                Console.WriteLine(jsonString);

                response.EnsureSuccessStatusCode();

                // Deserialize using Newtonsoft.Json
                return JsonConvert.DeserializeObject<List<ProductTypeResponse>>(jsonString);
            }

            public async Task<ProductTypeResponse> AddProductType(ProductTypeAddRequest productTypeAddRequest)
            {
                var response = await _httpClient.PostAsJsonAsync("http://host.docker.internal:8080/api/ProductType/", productTypeAddRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ProductTypeResponse>();
            }


            public async Task DeleteProductType(Guid id)
            {
                var response = await _httpClient.DeleteAsync($"http://host.docker.internal:8080/api/ProductType/{id}");
                response.EnsureSuccessStatusCode();
            }

            public async Task<ProductTypeResponse> UpdateProductType(ProductTypeUpdateRequest productTypeUpdateRequest)
            {
                var response = await _httpClient.PutAsJsonAsync("http://host.docker.internal:8080/api/ProductType/", productTypeUpdateRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ProductTypeResponse>();
            }
            #endregion
        }
    }
