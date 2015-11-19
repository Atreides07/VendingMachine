using System.Windows;
using VendingMachine.MVVM;

namespace VendingMachine.WPF.ViewModel
{
    public class UserNotify : IUserNotify
    {
        public void Alert(string message)
        {
            MessageBox.Show(message);
        }
    }
}