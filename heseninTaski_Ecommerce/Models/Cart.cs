using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace heseninTaski_Ecommerce.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>(); // Səbətdəki məhsullar
        public decimal TotalPrice => (decimal)Items.Sum(item => item.Product.Price * item.Quantity); // Ümumi qiymət

        public void AddToCart(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(item => item.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity; // Əgər məhsul varsa, miqdarı artırırıq
            }
            else
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity }); // Yeni məhsul əlavə edirik
            }
        }

        public void RemoveFromCart(int productId)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                Items.Remove(item); // Məhsulu səbətdən silirik
            }
        }
    }

    public class CartItem
    {
        public Product Product { get; set; } // Məhsul
        public int Quantity { get; set; } // Məhsulun miqdarı
    }
}
