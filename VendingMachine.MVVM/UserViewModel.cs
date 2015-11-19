using System.Collections.Generic;
using System.Linq;
using VendingMachine.LogicLayer;

namespace VendingMachine.MVVM
{
    public class UserViewModel : BaseViewModel
    {
        private readonly User user;
        public UserViewModel(User user)
        {
            this.user = user;
        }

        public IEnumerable<Coin> Purse => user.Purse.GetAllCoins();

        public IEnumerable<Product> Products => user.GetProducts().ToList();
    }
}