using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class LoanTransaction
    {
        public double Amount { get; }

        public LoanTransaction(double amount)
        {
            Amount = amount;
        }
    }
}
