﻿using asp1.Models;

namespace asp2.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine? line = Lines
            .Where(p => p.Product.ProductId == product.ProductId)
            .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveItem(Product product, int quantity)
        {
            CartLine? line = Lines
            .Where(p => p.Product.ProductId == product.ProductId)
            .FirstOrDefault();

            if (line.Quantity == 1)
            {
                RemoveLine(product);
            }
            else
            {
                line.Quantity -= quantity;
            }
        }
        public void RemoveLine(Product product) =>
            Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);
        public decimal ComputeTotalValue() =>
            (decimal)Lines.Sum(e => e.Product?.ProductPrice * e.Quantity);
        public decimal ComputeTax() 
        { 
            return (decimal)(Lines.Sum(e => e.Product?.ProductPrice * e.Quantity) * 1.1M);
        }
            //(decimal)Lines.Sum(e => e.Product?.ProductPrice * e.Quantity);
        public void Clear() => Lines.Clear();

    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
