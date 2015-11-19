using System;
using Windows.UI.Popups;
using VendingMachine.MVVM;

namespace VendingMachine.UWP.VIewModel
{
    public class UserNotify : IUserNotify

    {
        public async void Alert(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}