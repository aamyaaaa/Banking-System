using System;


namespace MultipleBankAccounts
{
    public enum MenuOption
    {
        Add,
        Withdraw,
        Deposit,
        find,
        Print,
        Transfer,
        PrintTransaction,
        Quit
    }

    class BankTransaction
    {
        static void DoDeposit(Account account, Bank BANK)
        {
            Console.Write("\nEnter the name of the account you want to deposit to: ");
            string accountName = Console.ReadLine();
            Console.Write("Enter the amount to deposit: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            try
            {
                Account accounts = BANK.GetAccount(accountName);
                //Account account = bank.FindAccount(accountName);
                if (accounts == null)
                {
                    throw new Exception("Account not found.");
                }
                
                DepositTransaction transaction = new DepositTransaction(accounts, amount);
                BANK.TransactionList.Add(transaction);
                transaction.Execute();
                transaction.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void DoWithdraw(Account account, Bank BANK)
        {
            Console.Write("\nEnter the name of the account you want to withdraw from: ");
            string accountName = Console.ReadLine();

            Console.Write("\nEnter the amount you want to withdraw: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            try
            {
                Account accounts = BANK.GetAccount(accountName);
                if (accounts == null)
                {
                    throw new Exception("Account not found.");
                }
                WithdrawTransaction transaction = new WithdrawTransaction(account, amount);
                BANK.TransactionList.Add(transaction);
                //BANK.AddTransaction(transaction);
                transaction.Execute();
                transaction.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DoPrint(Account account, Bank BANK)
        {
            Console.Write("\nEnter the name of the account you want to  print: ");
            string accountName = Console.ReadLine();

            account = BANK.GetAccount(accountName);
            account.print(account);
        }

        static void DoRollback(Bank BANK)
        {
            Console.Write("Do you wish to rollback a transaction? (Y/N): ");
            do
            {
                try
                {
                    string response = Console.ReadLine().ToLower();
                    if (response == "y")
                    {
                        ChooseRollback(BANK);
                        break;
                    }
                    else if (response == "n")
                    {
                        break;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch
                {
                    Console.WriteLine("The response is not valid");
                }
            }
            while (true);
        }

        private static void ChooseRollback(Bank BANK)
        {
            Console.Write("Which transaction do you wish to rollback: ");
            do
            {
                try
                {
                    int response = Convert.ToInt32(Console.ReadLine());
                    if (response < BANK.TransactionList.Count && response >= 0)
                    {
                        try
                        {
                            BANK.RollbackTransaction(BANK.TransactionList[response]);
                            BANK.Remove(response);
                            return;
                        }
                        catch (System.Exception exception)
                        {
                            Console.WriteLine("The following error detected: with message \"" + exception.Message + "\"");
                            Console.ReadKey();
                            return;
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("This operation could not be completed.");
                    }
                }
                catch
                {
                    Console.WriteLine("Please input a valid response");
                }
            }
            while (true);
        }

        static void DoTransfer(Account fromAccount, Account toAccount, Bank BANK)
        {
            decimal amount;
            Console.WriteLine("Enter the amount to transfer:");
            amount = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Write the name of the account from which you want to transfer ? ");
            fromAccount._name = Console.ReadLine();

            //Console.WriteLine("Write the name of the account from which you want to transfer ? ");
            //fromAccount._name = Console.ReadLine();

            //Console.WriteLine("Balance here?");
            //fromAccount._balance = Convert.ToDecimal(Console.ReadLine());

            //Console.WriteLine("Write the name of the account to which you want to transfer ? ");
            //toAccount._name = Console.ReadLine();
            //Console.WriteLine("Balance here?");
            //toAccount._balance = Convert.ToDecimal(Console.ReadLine());

            //Console.WriteLine("Enter the amount to transfer:");
            //amount = Convert.ToDecimal(Console.ReadLine());

            try
            {
                Account accounts = BANK.GetAccount(fromAccount._name);
                //Account accounts = BANK.GetAccount(toAccount._name);
                if (accounts == null)
                {
                    Console.WriteLine("Write the name of the account from which you want to transfer ? ");
                    fromAccount._name = Console.ReadLine();
                    Console.WriteLine("Balance here?");
                    fromAccount._balance = Convert.ToDecimal(Console.ReadLine());

                    throw new Exception("Account not found.");
                }
                else
                {
                    Console.WriteLine("Write the name of the account to which you want to transfer ? ");
                    toAccount._name = Console.ReadLine();
                    Account account = BANK.GetAccount(toAccount._name);
                    if (account == null)
                    {
                        Console.WriteLine("Write the name of the account to which you want to transfer ? ");
                        toAccount._name = Console.ReadLine();
                        Console.WriteLine("Balance here?");
                        toAccount._balance = Convert.ToDecimal(Console.ReadLine());
                    }

                }
                TransferTransaction transaction = new TransferTransaction(fromAccount, toAccount, amount);
                BANK.TransactionList.Add(transaction);
                transaction.Execute();
                transaction.Print();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }

        public static Account FindAccount(Bank BANK)
        {
            Console.Write("Enter account name: ");
            string accountName = Console.ReadLine();
            //Account account = BANK.GetAccount(accountName);
            Account accounts = BANK.GetAccount(accountName);

            if (accounts == null)
            {
                Console.WriteLine("Account " + accountName + " was not located");
            }
            else
            {
                accounts.print(accounts);
            }

            return accounts;
        }


        static void Main(string[] args)
        {
            Bank BANK = new Bank();
            string name;
            decimal balance;

            Console.WriteLine("What is your account name?");
            name = Console.ReadLine();
            Console.WriteLine("What is the balance in your account?");
            balance = Convert.ToDecimal(Console.ReadLine());
            Account initialAccount = new Account(name, balance);
            BANK.AddAccount(initialAccount);

            Account account = new Account(name, balance);
            Account account1 = new Account(name, balance);
            Account account2 = new Account(name, balance);

            MenuOption choice;
            do
            {
                choice = ReadUserOption();
                switch (choice)
                {
                    case MenuOption.Add:
                        //Bank BANK = new Bank();
                        Console.Write("Enter the account name: ");
                        string _name = Console.ReadLine();
                        Console.Write("Enter balance of the account: ");
                        decimal _balance = Convert.ToDecimal(Console.ReadLine());
                        Account newAccount = new Account(_name, _balance);
                        BANK.AddAccount(newAccount);
                        Console.WriteLine("New account added successfully!");
                        break;
                    case MenuOption.Withdraw:
                        DoWithdraw(account, BANK);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(account, BANK);
                        break;
                    case MenuOption.find:
                        FindAccount(BANK);
                        break;
                    case MenuOption.Print:
                        DoPrint(account, BANK);
                        break;
                    case MenuOption.PrintTransaction:
                        BANK.PrintTransactionHistory();
                        DoRollback(BANK);
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("\nExiting program.");
                        break;
                    case MenuOption.Transfer:
                        //Account fromAccount, toAccount;
                        DoTransfer(account, account, BANK);
                        break;
                    default:
                        Console.WriteLine("\nInvalid option.");
                        break;
                }
            } while (choice != MenuOption.Quit);

            Console.ReadLine();
        }

        static MenuOption ReadUserOption()
        {
            int userChoice;

            do
            {
                Console.WriteLine("\nPlease choose an option from the following menu:");
                Console.WriteLine("1. Add Account");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Find Account");
                Console.WriteLine("5. Print");
                Console.WriteLine("6. Transfer");
                Console.WriteLine("7. Print Transaction History");
                Console.WriteLine("8. Quit");

                Console.Write("\nEnter your choice: ");

                userChoice = Convert.ToInt32(Console.ReadLine());
            } while (userChoice <= 0 || userChoice >= 9);

            return (MenuOption)(userChoice - 1);
        }
    }
}
