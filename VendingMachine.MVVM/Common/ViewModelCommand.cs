using System;
using System.Windows.Input;

namespace VendingMachine.MVVM
{
    public class ViewModelCommand : ICommand
    {
        private readonly IUserNotify userNotify;
        private readonly Action action;

        public ViewModelCommand(IUserNotify userNotify, Action action)
        {
            this.userNotify = userNotify;
            this.action = action;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                action();
            }
            catch (Exception exp)
            {
                userNotify.Alert(exp.Message);
            }

        }

        public event EventHandler CanExecuteChanged;
    }

    public class ViewModelCommand<T> : ICommand
    {
        private readonly IUserNotify userNotify;
        private readonly Action<T> action;

        public ViewModelCommand(IUserNotify userNotify, Action<T> action)
        {
            this.userNotify = userNotify;
            this.action = action;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                action((T)parameter);
            }
            catch (Exception exp)
            {
                userNotify.Alert(exp.Message);
            }

        }

        public event EventHandler CanExecuteChanged;
    }
}