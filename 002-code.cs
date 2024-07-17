using System;
using MultipleBankAccounts;

namespace MultipleBankAccounts
{
    public class TransferTransaction: Transaction
    {
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;
        private Account _fromAccount;
        private Account _toAccount;

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount):base(amount)
        {
            _withdraw = new WithdrawTransaction(fromAccount, amount);
            _deposit = new DepositTransaction(toAccount, amount);
            _fromAccount = fromAccount;
            _toAccount = toAccount;
        }


        public bool Success
        {
            get { return _deposit.Success && _withdraw.Success; }
        }

        public override void Execute()
        {
            if (_withdraw.Success || _deposit.Success)
            {
                throw new InvalidOperationException("Transaction has been executed already");
            }

            _withdraw.Execute();

            if (_withdraw.Success)
            {
                _deposit.Execute();
            }
            else
            {
                throw new InvalidOperationException("the funds are insufficient");
            }
        }

        public override void Rollback()
        {
            if (!_withdraw.Success || !_deposit.Success)
            {
                throw new InvalidOperationException("Transaction is not successfully completed.");
            }

            try
            {
                _deposit.Rollback();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("TransferTransaction failed to rollback deposit transaction.", ex);
            }

            try
            {
                _withdraw.Rollback();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("TransferTransaction failed to rollback withdraw transaction.", ex);
            }
        }

        public override void Print()
        {
            Console.WriteLine(_deposit._amount + "Transferred from" + _fromAccount._name + " to " + _toAccount._name);
            _deposit.Print();
            _withdraw.Print();
        }
    }
}





