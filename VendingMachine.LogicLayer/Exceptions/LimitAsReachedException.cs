using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.LogicLayer.Exceptions
{
    public class LimitAsReachedException : Exception
    {
        public LimitAsReachedException(string message) : base(message)
        {
        }
    }
}
