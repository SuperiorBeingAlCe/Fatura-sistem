using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatura_sistem.Models
{
    internal class CartItem
    {
        private Product _product;
        private int _quantity;

        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public CartItem(Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
        }

        public decimal TotalPrice
        {
            get { return _product.Price * _quantity; }
        }

        public override string ToString()
        {
            return $"{_product.Name} x {_quantity} = {TotalPrice:C}";
        }
    }
}
