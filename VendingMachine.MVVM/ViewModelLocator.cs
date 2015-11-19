using System;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using VendingMachine.LogicLayer;

namespace VendingMachine.MVVM
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            
            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    return;
            //}
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var vendingMachine = GetVendingMachine();
            SimpleIoc.Default.Register(() => vendingMachine);
            var user = GetUser();
            SimpleIoc.Default.Register(() => user);
            SimpleIoc.Default.Register<MainViewModel>();

        }

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        private User GetUser()
        {
            var user = new User();
            user.Purse.SetCoins(1, 10);
            user.Purse.SetCoins(2, 30);
            user.Purse.SetCoins(5, 20);
            user.Purse.SetCoins(10, 15);
            return user;
        }

        private VendingMachineCore GetVendingMachine()
        {
            var vendingInitState = new VendingInitState();
            vendingInitState.SetCoins(1, 100);
            vendingInitState.SetCoins(2, 100);
            vendingInitState.SetCoins(5, 100);
            vendingInitState.SetCoins(10, 100);
            vendingInitState.AddProduct("Чай", 13, 10);
            vendingInitState.AddProduct("Кофе", 18, 20);
            vendingInitState.AddProduct("Кофе с молоком", 21, 20);
            vendingInitState.AddProduct("Сок", 35, 15);
            return new VendingMachineCore(vendingInitState);

            
        }
    }
}
