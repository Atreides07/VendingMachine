using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachine.LogicLayer.Exceptions;

namespace VendingMachine.LogicLayer
{
    public class VendingMachineCore
    {
        private readonly Dictionary<Guid,Product> products;
        public VendingMachineCore(VendingInitState vendingInitState)
        {
            Purse = vendingInitState.Purse;
            products = vendingInitState.Products.ToDictionary(p=>p.Id);
            UserMoneySumm = vendingInitState.StartUserSumm;
        }

        public Purse Purse { get; }

        public IEnumerable<Product> GetProducts()
        {
            return products.Values;
        }
        public int UserMoneySumm { get; private set; }
        public void AddCoinByUser(Purse purse, int nominal)
        {
            var coin = purse.TakeCoinByNominal(nominal);
            this.Purse.Add(coin);
            UserMoneySumm += nominal;
        }

        public Product ByeProduct(Guid productId)
        {
            if (products[productId].Count <= 0)
            {
                throw new LimitAsReachedException("Нет соответствующего товара в наличии");
            }
            if (products[productId].Price > UserMoneySumm)
            {
                throw new LimitAsReachedException("Недостаточно средств");
            }
            var countOfSaleProducts = 1;
            UserMoneySumm -= products[productId].Price* countOfSaleProducts;
            return products[productId].TakeProduct(countOfSaleProducts);
        }

        public void TakeChange(Purse toPurse)
        {
            var currenCoins = Purse.GetAllCoins().OrderByDescending(i => i.Nominal).ToList();
            var changeCoins = CalculateCoins(currenCoins, UserMoneySumm);
            foreach (var changeCoin in changeCoins)
            {
                Purse.Dec(changeCoin);
                toPurse.Add(changeCoin);
            }
        }

        private IEnumerable<Coin> CalculateCoins(List<Coin> currenCoins, int userMoneySumm)
        {
            foreach (var currenCoin in currenCoins)
            {
                var needCountCoins =userMoneySumm / currenCoin.Nominal;
                if (needCountCoins > currenCoin.Count)
                {
                    needCountCoins = currenCoin.Count;
                }
                userMoneySumm -= needCountCoins*currenCoin.Nominal;
                var calculateCoins = new Coin()
                {
                    Count = needCountCoins,
                    Nominal = currenCoin.Nominal
                };
                yield return calculateCoins;
                if (userMoneySumm == 0)
                {
                    yield break;
                }
            }
            throw new LimitAsReachedException("Нет необходимой суммы в кошельке");
        }
    }

    public class User
    {
        public User()
        {
            Purse=new Purse();
        }

        public Purse Purse { get; }
        private readonly Dictionary<Guid,Product> products=new Dictionary<Guid, Product>();

        public void AddProduct(Product product)
        {
            if (!products.ContainsKey(product.Id))
            {
                products[product.Id] = product;
            }
            else
            {
                products[product.Id].Increment(product.Count);
            }
            
        }

        public IEnumerable<Product> GetProducts()
        {
            return products.Values;
        }
    }
    
    public class Product
    {
        public Product(int count)
        {
            Count = count;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        
        public int Count { get; private set; }

        public Product TakeProduct(int count)
        {
            ValidationCount(count);
            if (count > Count)
            {
                throw new LimitAsReachedException("Нет необходимого количества товаров");
            }
            Count -= count;
            return CreateNewProduct(count);
        }

        public void Increment(int count)
        {
            ValidationCount(count);
            Count += count;
        }

        private void ValidationCount(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Отрицательное или нулевое количество товаров недопустимо");
            }
           
        }

        private Product CreateNewProduct(int count)
        {
            return new Product(count)
            {
                Price = this.Price,
                Id = this.Id,
                Name = this.Name
            };
        }
    }
    
    public class Purse
    {
        readonly Dictionary<int,int> cointCountByNominal=new Dictionary<int, int>();
        
        public void SetCoins(int nominal, int count)
        {
            //TODO: проврека на существования номинала
            cointCountByNominal[nominal] = count;
        }

        public IEnumerable<Coin> GetAllCoins()
        {
            return cointCountByNominal.Select(i=>new Coin()
            {
                Nominal = i.Key,
                Count = i.Value
            });
        }

        public int GetCoinCount(int nominal)
        {
            return cointCountByNominal[nominal];
        }

        public Coin TakeCoinByNominal(int nominal)
        {
            if (cointCountByNominal[nominal]<=0)
            {
                throw new RankException("Монет соответствующего номинала нет в кошельке");
            }
            cointCountByNominal[nominal]--;
            return new Coin()
            {
                Count = 1,
                Nominal = nominal
            };
        }

        public void Add(Coin coin)
        {
            cointCountByNominal[coin.Nominal] += coin.Count;
        }
        public void Dec(Coin coin)
        {
            cointCountByNominal[coin.Nominal] -= coin.Count;
        }
    }

    public struct Coin
    {
        public int Nominal { get; set; }
        public int Count { get; set; }
    }
}
