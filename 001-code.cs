using System;
using System.Security.Principal;

namespace MultipleBankAccounts
{
    public class WithdrawTransaction: Transaction
    {
        public Account _account;
        private decimal _amount;
        private bool _executed;
        private bool _success;
        private bool _reversed;


        public WithdrawTransaction(Account account, decimal amount):base(amount)
        {
            _account = account;
            _amount = amount;
            _executed = false;
            _success = false;
            _reversed = false;
   
        }
        public override void Print()
        {
            Console.WriteLine("Withdraw Transaction Details:");
            Console.WriteLine("Amount Withdrawn: " + _amount);
            Console.WriteLine("Executed: " + _executed);
            Console.WriteLine("Success: " + _success);
            Console.WriteLine("Reversed: " + _reversed);
        }

        public override void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transaction has already been executed.");
              
            }

            if (_account._balance < _amount)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            _account.Withdraw(_amount);
            _success = true;
            _executed = true;
        }

        public override void Rollback()
        {
            if (!_executed)
            {
                throw new InvalidOperationException("Transaction has not been executed yet.");
            }

            if (_reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }

            if (_success)
            {
                _account.Deposit(_amount);
                _reversed = true;
            }
            else
            {
                throw new InvalidOperationException("Transaction was not successful and cannot be reversed.");
            }
        }

        
    }
}






