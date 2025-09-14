using Ecommerce1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class OrderService
{
    private readonly HttpClient _httpClient;

    public OrderService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<bool> CreateOrderAsync(string userId, List<CartItem> cart, double totalPrice, string token)
    {
        try
        {
            var order = new
            {
                UserId = userId,
                Items = cart.Select(item => new
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity
                }).ToList(),
                TotalPrice = totalPrice
            };

            var jsonContent = JsonSerializer.Serialize(order);
            Console.WriteLine(jsonContent);  // Affiche le JSON dans la console pour débogage

            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Envoyer la requête HTTP POST
            var response = await _httpClient.PostAsync("http://10.0.2.2:8082/api/orders", httpContent);

            // Vérification de la réponse
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Order created successfully.");
                return true;
            }
            else
            {
                // Loguer les détails de la réponse
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response: {responseBody}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while creating order: {ex.Message}");
            return false;
        }
    }
}




