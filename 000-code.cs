using System;
using MultipleBankAccounts;

namespace MultipleBankAccounts
{
    public class DepositTransaction: Transaction
    {
        public readonly Account _account;
        public readonly decimal _amount;
        private bool _executed;
        private bool _success;
        private bool _reversed;
      

        public DepositTransaction(Account account, decimal amount):base(amount)
        {
            _account = account;
            _amount = amount;
   
        }

        //printing
        public override void Print()
        {
            Console.WriteLine("Transaction Type: Deposit");
            Console.WriteLine("Amount Deposited: " + _amount);
            Console.WriteLine("Transaction Status: " + Success);
        }

        public override void Execute()
        {
        
            if (_executed)
            {
                throw new InvalidOperationException("This transaction has already been executed.");
            }

            _executed = true;

            try
            {
                _account.Deposit(_amount);
                _success = true;
            }
            catch (Exception ex)
            {
                _success = false;
                throw new InvalidOperationException($"Failed to execute deposit transaction. Reason: {ex.Message}");
            }
        }

        public override void  Rollback()
        {
            
            if (!_executed)
            {
                throw new InvalidOperationException("This transaction has not been executed yet.");
            }

            if (_reversed)
            {
                throw new InvalidOperationException("This transaction has already been reversed.");
            }

            if (!_success)
            {
                throw new InvalidOperationException("This transaction was not successful and cannot be reversed.");
            }

            _reversed = true;

            try
            {
                _account.Withdraw(_amount);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to rollback deposit transaction. Reason: {ex.Message}");
            }
        }

       

    }
}




