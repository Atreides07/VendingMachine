using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using VendingMachine.MVVM;

namespace VendingMachine.UWP.VIewModel
{
    public class UwpViewModelLocator : ViewModelLocator
    {
        public UwpViewModelLocator() : base()
        {
            SimpleIoc.Default.Register<IUserNotify,UserNotify>();
        }

    }
}
