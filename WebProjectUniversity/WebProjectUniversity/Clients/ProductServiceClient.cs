
using Newtonsoft.Json;
using Ocelot.Infrastructure;
using ProductService.DTO;

namespace WebProjectUniversity.UI.Clients
{
    public class ProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region Product
        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:8080/api/product");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Log the JSON for inspection
            Console.WriteLine(jsonString);

            response.EnsureSuccessStatusCode();

            // Deserialize using Newtonsoft.Json
            return  JsonConvert.DeserializeObject<List<ProductResponse>>(jsonString);
        }

        public async Task<ProductResponse> GetProductByIdAsync(Guid id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductResponse>($"http://localhost:8080/api/product/{id}");
            return product;
        }

        public async Task<ProductResponse> AddProductAsync(ProductAddRequest product)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/product", product);
            response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ProductResponse>();

        }

        public async Task UpdateProductAsync(ProductUpdateRequest product)
        {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5178/api/product/{product.Id}", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5178/api/product/{id}");
            response.EnsureSuccessStatusCode();
        }

        #endregion


        #region Category
        public async Task<List<ProductCategoryResponse>> GetAllProductCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5178/api/productcategory");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Log the JSON for inspection
            Console.WriteLine(jsonString);

            response.EnsureSuccessStatusCode();

            // Deserialize using Newtonsoft.Json
            return JsonConvert.DeserializeObject<List<ProductCategoryResponse>>(jsonString);
        }


        public async Task<ProductCategoryResponse> GetProductCategoryByCategoryId(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductCategoryResponse>($"http://localhost:5178/api/productcategory/{id}");
            return response;
        }



        public async Task<ProductCategoryResponse> AddProductCategory(ProductCategoryAddRequest productCategoryAddRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5178/api/productcategory/", productCategoryAddRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        }


        public async Task DeleteProductCategory(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5178/api/productcategory/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<ProductCategoryResponse> UpdateProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest)
        {
            var response = await _httpClient.PutAsJsonAsync("http://localhost:5178/api/productcategory/", productCategoryUpdateRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        }

        #endregion
    }
}
