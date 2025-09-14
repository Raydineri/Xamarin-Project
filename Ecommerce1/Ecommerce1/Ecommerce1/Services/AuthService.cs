using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Ecommerce1.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://10.0.2.2:8082/api/auth";
        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        // Signup method
        public async Task<string> SignupAsync(string email, string password, string username)
        {
            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                username = username,
                email = email,
                password = password
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{BaseUrl}/register", content);

            if (response.IsSuccessStatusCode)
            {
                return "Signup successful!";
            }
            else
            {
                return "Signup failed!";
            }
        }

        // Login method
        public async Task<string> LoginAsync(string email, string password)
        {
            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                email = email,
                password = password
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{BaseUrl}/login", content);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Save the token to Application.Current.Properties (or a secure storage option)
                Application.Current.Properties["auth_token"] = token;  // Correct usage of Application.Current
                return token;  // Return the token for further use
            }
            else
            {
                return "Login failed!";
            }
        }

        // Get Auth Token from Application properties
        public string GetAuthToken()
        {
            if (Application.Current.Properties.ContainsKey("auth_token"))
            {
                return Application.Current.Properties["auth_token"].ToString();
            }
            return null;
        }
        public async Task<AuthResponse> AdminLogin(string email, string password)
        {
            var response = await _httpClient.PostAsync($"{BaseUrl}/admin-login?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}", null);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var jwtResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
                return new AuthResponse { IsSuccessful = true, Token = jwtResponse["token"] };
            }

            return new AuthResponse { IsSuccessful = false };
        }
    }

    public class AuthResponse
    {
        public bool IsSuccessful { get; set; }
        public string Token { get; set; }
    }
}

