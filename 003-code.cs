using System;
using System.Security.Principal;

namespace MultipleBankAccounts
{
    public class Transaction
    {
        private decimal _amount;
        public readonly bool _executed = false;
        private readonly bool _success=false;
        public readonly bool _reversed = false;
        public DateTime _dateStamp;

        public Transaction(decimal amount)
        {
            _amount = amount;
        
        }

        public bool Executed
        {
            get { return _executed; }

        }

        public bool Success
        {
            get { return _success; }
        }

        public bool Reversed
        {
            get { return _reversed; }
        }

        public DateTime Date
        {
            get { return Date; }
        }

        public virtual void Print()
        {
        }

        public virtual void Execute()
        {

            if (_executed)
            {
                _dateStamp = DateTime.Now;
            }
            else
            { 
                throw new InvalidOperationException("This transaction has already been executed.");
            }
        }

        public virtual void Rollback()
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


            _dateStamp = DateTime.Now;
            
        }

        public DateTime DateStamp
        {
            get
            {
                return _dateStamp;
            }
        }

    }
}

