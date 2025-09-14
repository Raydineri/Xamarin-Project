using Ecommerce1.Model;
using Ecommerce1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ecommerce1
{
    public partial class MainPage : ContentPage
    {
        private ProductService productService = new ProductService();

        public MainPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var products = await productService.GetProductsAsync();
            ProductsListView.ItemsSource = products;
        }

        private async void AddToCart_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button.BindingContext as Product;

            if (product != null)
            {
                // Demander la quantité à l'utilisateur
                string result = await DisplayPromptAsync("Quantity", $"Enter the quantity for {product.Name}:",
                                                          initialValue: "1",
                                                          keyboard: Keyboard.Numeric);
                if (!string.IsNullOrEmpty(result) && int.TryParse(result, out int quantity) && quantity > 0)
                {
                    // Récupérer ou initialiser le panier
                    if (!Application.Current.Properties.ContainsKey("cart"))
                    {
                        Application.Current.Properties["cart"] = new List<CartItem>();
                    }

                    var cart = Application.Current.Properties["cart"] as List<CartItem>;

                    // Vérifier si le produit est déjà dans le panier
                    var existingItem = cart.FirstOrDefault(item => item.Product.Id == product.Id);
                    if (existingItem == null)
                    {
                        // Ajouter un nouvel élément au panier avec la quantité donnée
                        cart.Add(new CartItem
                        {
                            Product = product,
                            Quantity = quantity
                        });
                        await DisplayAlert("Success", $"{quantity} of {product.Name} has been added to your cart!", "OK");
                    }
                    else
                    {
                        // Augmenter la quantité si le produit existe déjà
                        existingItem.Quantity += quantity;
                        await DisplayAlert("Info", $"{product.Name} quantity has been updated to {existingItem.Quantity}.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Invalid quantity entered. Please try again.", "OK");
                }
            }
        }


        private async void OpenCart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShoppingCart());


        }

        

    }
}