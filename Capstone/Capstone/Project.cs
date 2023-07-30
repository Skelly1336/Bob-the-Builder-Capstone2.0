using System;
using System.Collections.Generic;

namespace PortfolioManagement

{
    /// <summary>
    /// Enum to represent different types of projects
    /// </summary>
    public enum ProjectType
    {
        None = -1,
        Build = 1,
        Renovation = 2
    }

    /// <summary>
    /// Abstract class representing a generic project
    /// </summary>
    abstract class Project
    {
        // Properties
        public string Name { get; protected set; }
        protected List<Transaction> transactions;

        // Constructor
        public Project(string name)
        {
            Name = name;
            transactions = new List<Transaction>();
        }

        /// <summary>
        /// Method to add a transaction to the project
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            Console.WriteLine("Transaction added");
        }

        /// <summary>
        /// Method to display transactions based on a provided filter
        /// </summary>
        /// <param name="filter"></param>
        public void DisplayTransactions(Func<Transaction, bool> filter)
        {
            Console.WriteLine("List of transactions for {0}:", Name);
            foreach (Transaction transaction in transactions)
            {
                if (filter(transaction))
                {
                    Console.WriteLine(transaction);
                }
            }
        }


        /// <summary>
        /// Method to calculate the total amount of transactions based on a provided filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        public decimal GetTotalAmount(Func<Transaction, bool> filter)
        {
            decimal total = 0;
            foreach (Transaction transaction in transactions)
            {
                if (filter(transaction))
                {
                    total += transaction.Amount;
                }
            }
            return total;
        }

        // Abstract methods to be implemented by derived classes
        public abstract void DisplayProjectType();
        public abstract void DisplaySummary();
    }

    /// <summary>
    /// Class representing a new build project, derived from the Project class
    /// </summary>
    class NewBuildProject : Project
    {
        // Constructor
        public NewBuildProject(string name) : base(name)
        {
        }

        /// <summary>
        /// Implementation of the abstract method to display project type
        /// </summary>
        public override void DisplayProjectType()
        {
            Console.WriteLine("Project Type: New Build");
        }

        /// <summary>
        /// Implementation of the abstract method to display project summary
        /// </summary>
        public override void DisplaySummary()
        {
            decimal salesTotal = GetTotalAmount(transaction => transaction.Type is TransactionType.Sale);
            decimal purchasesTotal = GetTotalAmount(transaction => transaction.Type is TransactionType.Purchase);
            decimal profits = salesTotal - purchasesTotal;
            float taxRefund = CalculateTaxRefund();

            Console.WriteLine("Summary total for {0}:", Name);
            Console.WriteLine("Sales: {0}", salesTotal);
            Console.WriteLine("Purchases: {0}", purchasesTotal);
            Console.WriteLine("Profits: {0}", profits);
            Console.WriteLine("Expected tax refund: {0}", taxRefund);
        }

        /// <summary>
        /// Method to calculate the tax refund for the new build project
        /// </summary>
        /// <returns></returns>
        public float CalculateTaxRefund()
        {
            float salesTotal = (float)GetTotalAmount(transaction => transaction.Type is TransactionType.Sale);
            float purchasesTotal = (float)GetTotalAmount(transaction => transaction.Type is TransactionType.Purchase);
            float profits = salesTotal - purchasesTotal;
            return purchasesTotal - (purchasesTotal / 1.2f);
        }
    }

    /// <summary>
    /// Class representing a renovation project, derived from the Project class
    /// </summary>
    class RenovationProject : Project
    {
        // Constructor
        public RenovationProject(string name) : base(name)
        {
        }

        /// <summary>
        /// Implementation of the abstract method to display project type
        /// </summary>
        public override void DisplayProjectType()
        {
            Console.WriteLine("Project Type: Renovation");
        }

        /// <summary>
        /// Implementation of the abstract method to display project summary
        /// </summary>
        public override void DisplaySummary()
        {
            decimal salesTotal = GetTotalAmount(transaction => transaction.Type is TransactionType.Sale);
            decimal purchasesTotal = GetTotalAmount(transaction => transaction.Type is TransactionType.Purchase);
            decimal profits = salesTotal - purchasesTotal;

            Console.WriteLine("Summary total for {0}:", Name);
            Console.WriteLine("Sales: {0}", salesTotal);
            Console.WriteLine("Purchases: {0}", purchasesTotal);
            Console.WriteLine("Profits: {0}", profits);
        }
    }
}
