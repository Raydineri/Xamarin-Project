using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce1.Model
{
    public class Category
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Products")]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
