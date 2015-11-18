using System;
using System.Collections.Generic;

namespace VendingMachine.LogicLayer
{
    public class VendingInitState
    {
        public Purse Purse => purse;
        public IEnumerable<Product> Products => products;
        private readonly Purse purse=new Purse();
        public void SetCoins(int nominal, int count)
        {
            purse.SetCoins(nominal, count);
        }

        private readonly List<Product> products=new List<Product>();
        public Product AddProduct(string name, int price, int count)
        {
            //
            var product = new Product(count)
            {
                Id=Guid.NewGuid(),
                Name=name,
                Price =price,
                
            };
            products.Add(product);
            return product;
        }

        public int StartUserSumm { get; set; }
    }
}