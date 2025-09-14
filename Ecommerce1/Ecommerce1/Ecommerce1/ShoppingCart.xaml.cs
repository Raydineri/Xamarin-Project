using Ecommerce1.Model;
using Ecommerce1.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ecommerce1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingCart : ContentPage
    {
        private List<CartItem> Cart;

        public ShoppingCart()
        {
            InitializeComponent();
            LoadCart();
        }

        private void SaveCart()
        {
            Application.Current.Properties["cart"] = Cart;
            Application.Current.SavePropertiesAsync();
        }

        private void LoadCart()
        {
            if (Application.Current.Properties.ContainsKey("cart"))
            {
                Cart = Application.Current.Properties["cart"] as List<CartItem>;
            }
            else
            {
                Cart = new List<CartItem>();
            }

            CartCollectionView.ItemsSource = Cart;
            UpdateTotalPrice();
            UpdateCheckoutButtonState();  // Mettre à jour l'état du bouton Checkout
        }

        private void UpdateCheckoutButtonState()
        {
            // Active le bouton Checkout seulement s'il y a des éléments dans le panier
            CheckoutButton.IsEnabled = Cart.Count > 0;
        }

        private void UpdateTotalPrice()
        {
            var total = Cart.Sum(item => item.Product.Price * item.Quantity);
            TotalPriceLabel.Text = $"Total: ${total:F2}";
        }

        private async void RemoveFromCart_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var cartItem = button.BindingContext as CartItem;

            if (cartItem != null)
            {
                Cart.Remove(cartItem);
                SaveCart();
                CartCollectionView.ItemsSource = null;
                CartCollectionView.ItemsSource = Cart;
                UpdateTotalPrice();
                UpdateCheckoutButtonState();  // Vérifiez à nouveau si le panier est vide
                await DisplayAlert("Removed", $"{cartItem.Product.Name} has been removed from your cart.", "OK");
            }
        }

        private void IncrementQuantity_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var cartItem = button.BindingContext as CartItem;

            if (cartItem != null)
            {
                cartItem.Quantity++;
                CartCollectionView.ItemsSource = null;
                CartCollectionView.ItemsSource = Cart;
                UpdateTotalPrice();
                UpdateCheckoutButtonState();  // Mettre à jour l'état du bouton
            }
        }

        private void DecrementQuantity_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var cartItem = button.BindingContext as CartItem;

            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                CartCollectionView.ItemsSource = null;
                CartCollectionView.ItemsSource = Cart;
                UpdateTotalPrice();
                UpdateCheckoutButtonState();  // Mettre à jour l'état du bouton
            }
            else if (cartItem.Quantity == 1)
            {
                DisplayAlert("Attention", "La quantité ne peut pas être inférieure à 1. Utilisez 'Remove' pour supprimer l'article.", "OK");
            }
        }

        private async void OnCheckoutButtonClicked(object sender, EventArgs e)
        {
            var token = Application.Current.Properties.ContainsKey("auth_token") ?
                        Application.Current.Properties["auth_token"].ToString() : null;

            if (string.IsNullOrEmpty(token))
            {
                await DisplayAlert("Error", "You must be logged in to proceed with the checkout.", "OK");
                return;
            }

            string userId = GetUserIdFromToken(token);

            if (string.IsNullOrEmpty(userId))
            {
                await DisplayAlert("Error", "Unable to retrieve user ID from token.", "OK");
                return;
            }

            var orderService = new OrderService();
            double totalPrice = Cart.Sum(item => item.Product.Price * item.Quantity);

            bool success = await orderService.CreateOrderAsync(userId, Cart, totalPrice, token);

            if (success)
            {
                await DisplayAlert("Success", "Your order has been placed successfully!", "OK");

                Cart.Clear();
                Application.Current.Properties["cart"] = Cart;

                CartCollectionView.ItemsSource = null;
                CartCollectionView.ItemsSource = Cart;

                UpdateTotalPrice();
                UpdateCheckoutButtonState();  // Mettre à jour l'état du bouton après la commande
            }
            else
            {
                await DisplayAlert("Error", "Failed to place order. Please try again.", "OK");
            }
        }

        private string GetUserIdFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

                    if (!string.IsNullOrEmpty(userId))
                    {
                        return userId;
                    }
                    else
                    {
                        Console.WriteLine("User ID not found in the token.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing token: {ex.Message}");
            }

            return null;
        }
    }
}
