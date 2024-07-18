using System;
using System.Collections.Generic;

class BankAccount
{
    public string AccountNumber { get; set; }
    public string OwnerName { get; set; }
    public decimal Balance { get; private set; }

    public BankAccount(string accountNumber, string ownerName, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        OwnerName = ownerName;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > Balance)
            return false;
        Balance -= amount;
        return true;
    }
}

class BankSystem
{
    private List<BankAccount> accounts = new List<BankAccount>();

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    DepositMoney();
                    break;
                case "3":
                    WithdrawMoney();
                    break;
                case "4":
                    CheckBalance();
                    break;
                case "5":
                    return ;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Bank System Menu ===");
        Console.WriteLine("1. Create a new account");
        Console.WriteLine("2. Deposit money");
        Console.WriteLine("3. Withdraw money");
        Console.WriteLine("4. Check balance");
        Console.WriteLine("5. Exit");
        Console.Write("Enter your choice (1-5): ");
    }

    private void CreateAccount()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter owner name: ");
        string ownerName = Console.ReadLine();
        Console.Write("Enter initial balance: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
        {
            accounts.Add(new BankAccount(accountNumber, ownerName, initialBalance));
            Console.WriteLine("Account created successfully.");
        }
        else
        {
            Console.WriteLine("Invalid balance amount.");
        }
    }

    private void DepositMoney()
    {
        BankAccount account = FindAccount();
        if (account == null) return;

        Console.Write("Enter deposit amount: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            account.Deposit(amount);
            Console.WriteLine($"Deposit successful. New balance: {account.Balance:C}");
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    private void WithdrawMoney()
    {
        BankAccount account = FindAccount();
        if (account == null) return;

        Console.Write("Enter withdrawal amount: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            if (account.Withdraw(amount))
                Console.WriteLine($"Withdrawal successful. New balance: {account.Balance:C}");
            else
                Console.WriteLine("Insufficient funds.");
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    private void CheckBalance()
    {
        BankAccount account = FindAccount();
        if (account == null) return;

        Console.WriteLine($"Current balance: {account.Balance:C}");
    }

    private BankAccount FindAccount()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        BankAccount account = accounts.Find(a => a.AccountNumber == accountNumber);
        if (account == null)
            Console.WriteLine("Account not found.");
        return account;
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankSystem bankSystem = new BankSystem();
        bankSystem.Run();
    }
}