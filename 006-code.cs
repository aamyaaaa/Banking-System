using System;
namespace MultipleBankAccounts
{
    public class Account
    {
        public string _name;
        public decimal _balance;
       

        public Account(string _name, decimal _balance)
        {
            this._balance = _balance;
            this._name = _name;
            //this._time = _time;
        }

        //Accessor method
        public string get_name()
        {
            return this._name;
        }
        public string get_balance()
        {
            return this._balance.ToString("");
        }

        //methods
        public void Deposit(decimal amount)
        {
            //decimal amount;
            //Console.WriteLine("How much money do you want to Deposit?");
            //amount = Convert.ToDecimal(Console.ReadLine());

            if (validatingdeposit(amount))
            {
                _balance = amount + this._balance;
                Console.WriteLine("Amount after deposit: " + _balance);
            }
            else
            {
                Console.WriteLine("You cannot deposit negative amounts");
            }
        }

        public void Withdraw(decimal amount)
        {
            //decimal amount;
            //Console.WriteLine("How much money do you want to Withdraw?");
            //amount = Convert.ToDecimal(Console.ReadLine());

            if (validatingwithdrawl(amount))
            {
                _balance = _balance - amount;
                Console.WriteLine("Amount after withdraw: " + _balance);
            }
            else
            {
                Console.WriteLine("Try again");
            }

        }

        //to print the details
        public void print(Account account)
        {
            //Console.WriteLine("name: " + this._name + "\nPrevious Balance: " + this._balance);
            Console.WriteLine("name: " + account._name + "\nPrevious Balance: " + account._balance);
        }

        //validating the deposit 
        public bool validatingdeposit(decimal amount)
        {
            if (amount >= 0)
            {
                Console.WriteLine("True");
                return true;
            }
            else
            {
                Console.WriteLine("False");
                return false;
            }
        }

        //validating the withdrawl
        public bool validatingwithdrawl(decimal amount)
        {
            if (_balance - amount >= 0 & amount >= 0)
            {
                Console.WriteLine("True");
                return true;

            }
            else
            {
                Console.WriteLine("False");
                return false;

            }
        }


    }
}


