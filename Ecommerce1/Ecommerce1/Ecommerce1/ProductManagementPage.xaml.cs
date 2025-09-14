using Ecommerce1.Model;
using Ecommerce1.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ecommerce1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductManagementPage : ContentPage
    {
        private readonly ProductService _productService;
        public ObservableCollection<Product> Products { get; set; }

        public ProductManagementPage()
        {
            InitializeComponent();
            _productService = new ProductService();
            Products = new ObservableCollection<Product>();
            BindingContext = this;
            LoadProducts();
        }
        private async void LoadProducts()
        {
            var products = await _productService.GetProductsAsync();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                await DisplayAlert("Error", "Please fill in all fields with valid data", "OK");
                return;
            }

            var newProduct = new Product
            {
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,
                Price = double.Parse(PriceEntry.Text),
                Quantity = int.Parse(QuantityEntry.Text),
                Image = ImageEntry.Text,
                Category = CategoryEntry.Text
            };

            await _productService.AddProductAsync(newProduct);
            LoadProducts();
            ClearEntries();
        }

        private async void OnUpdateProductClicked(object sender, EventArgs e)
        {
            if (ProductList.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please select a product to update", "OK");
                return;
            }

            if (!ValidateInputs())
            {
                await DisplayAlert("Error", "Please fill in all fields with valid data", "OK");
                return;
            }

            var selectedProduct = (Product)ProductList.SelectedItem;
            selectedProduct.Name = NameEntry.Text;
            selectedProduct.Description = DescriptionEntry.Text;
            selectedProduct.Price = double.Parse(PriceEntry.Text);
            selectedProduct.Quantity = int.Parse(QuantityEntry.Text);
            selectedProduct.Image = ImageEntry.Text;
            selectedProduct.Category = CategoryEntry.Text;

            await _productService.UpdateProductAsync(selectedProduct.Id, selectedProduct);
            LoadProducts();
            ClearEntries();
        }

        private async void OnDeleteProductClicked(object sender, EventArgs e)
        {
            if (ProductList.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please select a product to delete", "OK");
                return;
            }

            var selectedProduct = (Product)ProductList.SelectedItem;
            bool answer = await DisplayAlert("Confirm", $"Are you sure you want to delete {selectedProduct.Name}?", "Yes", "No");
            if (answer)
            {
                await _productService.DeleteProductAsync(selectedProduct.Id);
                LoadProducts();
                ClearEntries();
            }
        }

        private void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedProduct = (Product)e.SelectedItem;
                IdEntry.Text = selectedProduct.Id;
                NameEntry.Text = selectedProduct.Name;
                DescriptionEntry.Text = selectedProduct.Description;
                PriceEntry.Text = selectedProduct.Price.ToString();
                QuantityEntry.Text = selectedProduct.Quantity.ToString();
                ImageEntry.Text = selectedProduct.Image;
                CategoryEntry.Text = selectedProduct.Category;
            }
        }

        private void ClearEntries()
        {
            IdEntry.Text = string.Empty;
            NameEntry.Text = string.Empty;
            DescriptionEntry.Text = string.Empty;
            PriceEntry.Text = string.Empty;
            QuantityEntry.Text = string.Empty;
            ImageEntry.Text = string.Empty;
            CategoryEntry.Text = string.Empty;
        }

        private bool ValidateInputs()
        {
            return !string.IsNullOrEmpty(NameEntry.Text) &&
                   !string.IsNullOrEmpty(DescriptionEntry.Text) &&
                   double.TryParse(PriceEntry.Text, out _) &&
                   int.TryParse(QuantityEntry.Text, out _) &&
                   !string.IsNullOrEmpty(ImageEntry.Text) &&
                   !string.IsNullOrEmpty(CategoryEntry.Text);
        }
    }
}