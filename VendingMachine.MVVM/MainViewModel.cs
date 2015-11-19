using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using VendingMachine.LogicLayer;

namespace VendingMachine.MVVM
{
    public class MainViewModel : BaseViewModel
    {
        private readonly VendingMachineCore vendingMachine;
        private readonly User user;
        private readonly IUserNotify userNotify;

        public MainViewModel(VendingMachineCore vendingMachine, User user, IUserNotify userNotify)
        {
            this.vendingMachine = vendingMachine;
            this.user = user;
            this.userNotify = userNotify;
            UserViewModel = new UserViewModel(user);
            VendingMachineViewModel=new VendingMachineViewModel(vendingMachine);
        }

        public UserViewModel UserViewModel { get; private set; }
        public VendingMachineViewModel VendingMachineViewModel { get; private set; }

        public ICommand AddCointToVendingMachineCommand
        {
            get
            {
                return new ViewModelCommand<Coin>(userNotify, coin =>
                {
                    vendingMachine.AddCoinByUser(user.Purse,coin.Nominal);
                    OnPropertyChanged(nameof(UserViewModel));
                    OnPropertyChanged(nameof(VendingMachineViewModel));
                });
            }
        }

        public ICommand BuyPrductCommand
        {
            get
            {
                return new ViewModelCommand<Product>(userNotify,p =>
                {
                    var product=vendingMachine.BuyProduct(p.Id);
                    user.AddProduct(product);
                    OnPropertyChanged(nameof(UserViewModel));
                    OnPropertyChanged(nameof(VendingMachineViewModel));
                    OnPropertyChanged("VendingMachineViewModel.Products");
                });
            }
        }

        public ICommand TakeChangeCommand
        {
            get
            {
                return new ViewModelCommand(userNotify, () =>
                {
                    vendingMachine.TakeChange(user.Purse);
                    OnPropertyChanged(nameof(UserViewModel));
                    OnPropertyChanged(nameof(VendingMachineViewModel));
                });
            }
        }
    }
}