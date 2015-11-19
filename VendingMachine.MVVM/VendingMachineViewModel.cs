using System.Collections.Generic;
using VendingMachine.LogicLayer;

namespace VendingMachine.MVVM
{
    public class VendingMachineViewModel
    {
        private readonly VendingMachineCore vendingMachine;

        public VendingMachineViewModel(VendingMachineCore vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }


        public IEnumerable<Coin> Purse => vendingMachine.Purse.GetAllCoins();

        public IEnumerable<Product> Products => vendingMachine.GetProducts();
        public string UserSummViewModel => $"{vendingMachine.UserMoneySumm}р.";
    }
}