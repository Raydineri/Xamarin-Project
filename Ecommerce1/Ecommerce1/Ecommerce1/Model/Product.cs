using Newtonsoft.Json;

namespace Ecommerce1.Model
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }
}