using Ecommerce1.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce1.Services
{
    class ProductService
    {
        private readonly string BaseUrl = "http://10.0.2.2:8082/api/products";
        private readonly HttpClient client;

        public ProductService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "votre_jeton");

        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await client.GetStringAsync(BaseUrl);
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            var response = await client.GetStringAsync($"{BaseUrl}/{id}");
            return JsonConvert.DeserializeObject<Product>(response);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BaseUrl, content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(responseBody);
        }

        public async Task<Product> UpdateProductAsync(string id, Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/{id}", content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(responseBody);
        }

        public async Task DeleteProductAsync(string id)
        {
            var response = await client.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}