using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fatura_sistem.Models;

namespace Fatura_sistem.Services
{
    internal class InvoiceService
    {
        private List<CartItem> _cartItems;

        public InvoiceService()
        {
            _cartItems = new List<CartItem>();
        }

        public void AddToCart(Product product, int quantity)
        {
            _cartItems.Add(new CartItem(product, quantity));
        }

        public List<CartItem> GetCartItems()
        {
            return _cartItems;
        }

        public decimal GetTotalAmount()
        {
            decimal total = 0;
            foreach (var item in _cartItems)
            {
                total += item.TotalPrice;
            }
            return total;
        }
        }
}
