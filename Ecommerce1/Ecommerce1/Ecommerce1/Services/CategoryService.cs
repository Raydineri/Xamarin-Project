using Ecommerce1.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce1.Services
{
    public class CategoryService
    {
        private readonly string BaseUrl = "http://10.0.2.2:8082/api/Category";
        private readonly HttpClient client;

        public CategoryService()
        {
            client = new HttpClient();
            // Uncomment and replace with your actual token if you're using JWT authentication
            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "your_actual_token_here");
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var response = await client.GetAsync($"{BaseUrl}/Categories");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Category>>(content);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        // Implement similar error handling for other methods (AddCategoryAsync, UpdateCategoryAsync, DeleteCategoryAsync)
        // ...
    }
}

