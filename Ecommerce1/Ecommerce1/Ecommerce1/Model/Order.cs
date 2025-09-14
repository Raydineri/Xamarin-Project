using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce1.Model
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string status { get; set; }
    }
}
