using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatura_sistem.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name} - {Price} ₺";
        }
    }
}
