using System;
namespace MultipleBankAccounts
{
    public class Bank
    {
        private List<Account> accounts = new List<Account>();
        private List<Transaction> _transactions;

        public Bank()
        {
            _transactions = new List<Transaction>();
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }


        public Account GetAccount(string name)
        {
            foreach (Account account in accounts)
            {
                if (account._name == name)
                {
                    return account;
                }
            }

            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            _transactions.Add(transaction);
        }

        public void RollbackTransaction(Transaction transaction)
        {
            transaction.Rollback();
            _transactions.Add(transaction);
        }

        public void PrintTransactionHistory()
        {
            Console.Clear();
            for (int i = 0; i < _transactions.Count; i += 1)
            {
                Console.WriteLine(" Transaction Number: " + i);
                _transactions[i].Print();
                Console.WriteLine("Success: " + _transactions[i].Success);
                Console.WriteLine("Executed: " + _transactions[i].Executed);
                Console.WriteLine("Reversed: " + _transactions[i].Reversed);
               
            }
            Console.WriteLine("------");
            Console.ReadKey();
        }

        public void Remove(int indexToDelete)
        {
            _transactions[indexToDelete] = _transactions[_transactions.Count - 1];
            _transactions.Remove(_transactions[_transactions.Count - 1]);
        }

        public List<Transaction> TransactionList
        {
            get
            {
                return _transactions;
            }
        }



    }
}

