# Capstone Project Markdown

## Table of Contents
- [Introduction](#introduction)
- [Fundamentals](#fundamentals)
- [Abstracting Responsibilities](#abstracting-responsibilities)
- [Encapsulation](#encapsulation)
- [Avoiding Code Repetition Through Inheritance](#avoiding-code-repetition-through-inheritance)
  - [NewBuildProject](#newbuildproject)
  - [RenovationProject](#renovationproject)
- [Runtime Polymorphism](#runtime-polymorphism)
  - [DisplayProjectType](#displayprojecttype)
  - [DisplaySummary](#displaysummary)
- [Main Functionality](#main-functionality)
  - [Adding a Project](#adding-a-project)
  - [Displaying Projects](#displaying-projects)
  - [Selecting a Project](#selecting-a-project)
  - [Adding a Transaction](#adding-a-transaction)
  - [Removing a Project](#removing-a-project)
  - [Displaying Transactions](#displaying-transactions)
  - [Displaying Project Summary](#displaying-project-summary)
  - [Displaying Portfolio Summary](#displaying-portfolio-summary)
  - [Loading Data](#loading-data)
- [Conclusion](#conclusion)

## Introduction
In this markdown file, I will be exploring my portfolio management system, designed to efficiently handle a wide array of projects and transactions. My code has been meticulously crafted to ensure that it adheres to several key principles of object-oriented programming, including the correct use of classes for abstracting responsibilities, encapsulation, avoiding code repetition through inheritance, and the use of runtime polymorphism.

## Fundamentals
The three primary classes in the program are Project, Transactions, and Portfolio. I have made a flexible framework for managing diverse project types thanks to the harmonious management of projects and associated financial transactions by these classes.

## Abstracting Responsibilities
My usage of abstract classes is the first thing that you should notice. A couple of derived classes, NewBuildProject and RenovationProject, are specifically based on the abstract Project class. This design decision makes it possible to share common functions while implementing project-specific information and behaviours independently for various project kinds.

```csc
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
    }
```

## Encapsulation
The code then shows effective encapsulation. For instance, the Transaction class's properties are designated as private, while public methods let users access and change their values. This prevents unauthorised access to a transaction's internal data, preserves data integrity, and permits limited change via well-specified interfaces.

```csc
    /// <summary>
    /// Define a class called Transaction to represent a financial transaction
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Properties to store transaction details
        /// </summary>
        public DateTime Date { get; private set; } // Date and time of the transaction
        public decimal Amount { get; private set; } // Amount of money involved in the transaction
        public string Description { get; private set; } // Description or reason for the transaction
        public TransactionType Type { get; private set; } // Type of the transaction (Sale or Purchase)

        /// <summary>
        /// Constructor to create a new Transaction object with provided details
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        public Transaction(decimal amount, string description, TransactionType type)
        {
            // Set the current date and time as the transaction date
            Date = DateTime.Now;

            // Set the amount and description of the transaction
            Amount = amount;
            Description = description;

            // Set the type of the transaction (Sale or Purchase)
            Type = type;
        }

        /// <summary>
        /// Method to display transaction details to the console
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Returns the transaction details as a string
            return $"Transaction '{Type.ToString()}' {Date}: {Description} ({Amount.ToString("0.00")})";
        }
    }
```

## Avoiding Code Repetition Through Inheritance
The ability to reduce code repetition provided by inheritance is a huge benefit. The abstract Project class's methods and properties are inherited by the NewBuildProject and RenovationProject classes, allowing for effective code reuse. Only the unique aspects of each individual project type need to be implemented; the common behaviours are taken directly from the abstract base class.

### NewBuildProject
```csc
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
```

### RenovationProject
```csc
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
```

## Runtime Polymorphism
By utilising runtime polymorphism, the code enables several DisplayProjectType and DisplaySummary implementations for NewBuildProject and RenovationProject. This dynamic behaviour improves flexibility and extensibility by allowing the program to choose the appropriate method to call based on the object's actual type at runtime. Refer to NewBuildProject and RenovationProject above.

### DisplayProjectType
```csc
        /// <summary>
        /// Method to display a summary of the selected project.
        /// </summary>
        public void DisplayProjectSummary()
        {
            if (selectedProjectIndex == -1)
            {
                Console.WriteLine("No project selected");
                return;
            }

            projects[selectedProjectIndex].DisplayProjectType(); // Display the type of the selected project (new build or renovation).
            projects[selectedProjectIndex].DisplaySummary(); // Display a summary of the selected project's transactions.
        }
```

### DisplaySummary
```csc
        /// <summary>
        /// Method to display a summary of the entire portfolio.
        /// </summary>
        public void DisplayPortfolioSummary()
        {
            decimal salesTotal = 0;
            decimal purchasesTotal = 0;
            float taxRefundableAmount = 0;

            // Calculate totals and tax refundable amount for each project.
            foreach (Project project in projects)
            {
                salesTotal += project.GetTotalAmount(transaction => transaction.Type is TransactionType.Sale); // Calculate total sales for each project.
                purchasesTotal += project.GetTotalAmount(transaction => transaction.Type is TransactionType.Purchase); // Calculate total purchases for each project.

                // If the project is a new build, add its total purchases to the tax refundable amount.
                if (project is NewBuildProject)
                {
                    taxRefundableAmount += (float)project.GetTotalAmount(transaction => transaction.Type is TransactionType.Purchase);
                }
            }

            decimal profits = salesTotal - purchasesTotal; // Calculate the overall profits for the portfolio.
            float taxRefund = taxRefundableAmount - (taxRefundableAmount / 1.2f); // Calculate the expected tax refund.

            // Display the portfolio summary.
            Console.WriteLine("Summary total for portfolio:");
            Console.WriteLine("Sales: {0}", salesTotal);
            Console.WriteLine("Purchases: {0}", purchasesTotal);
            Console.WriteLine("Profits: {0}", profits);
            Console.WriteLine("Expected tax refund: {0}", taxRefund);
        }
```

## Main Functionality
Let's dive into the key functionalities located in my PortfolioManager class. This class acts as the heart of the system, orchestrating the management of projects and transactions.

```csc
static void Main(string[] args)
        {
            PortfolioManager portfolioManager = new PortfolioManager();
            bool running = true;

            // Display the main menu for the portfolio management system

            while (running)
            {
                Console.WriteLine("Portfolio Management System");
                Console.WriteLine("1. Add a project");
                Console.WriteLine("2. Show list of existing projects");
                Console.WriteLine("3. Select a project");
                Console.WriteLine("4. Add a transaction to the selected project");
                Console.WriteLine("5. Remove the selected project");
                Console.WriteLine("6. Display all sales transactions for the selected project");
                Console.WriteLine("7. Display all purchase transactions for the selected project");
                Console.WriteLine("8. Display summary total for the selected project");
                Console.WriteLine("9. Display summary total for the entire portfolio");
                Console.WriteLine("10. Load data from Beige software report");
                Console.WriteLine("0. Exit");

                // Prompt the user to enter a menu option
                Console.Write("Enter menu option: ");
                int option = int.Parse(Console.ReadLine());
                Console.Clear();

                // Use a switch statement to handle user input based on the chosen option
                switch (option)
                {
                    case 1:
                        // Option 1: Add a new project to the portfolio
                        portfolioManager.AddProject();
                        break;
                    case 2:
                        // Option 2: Show the list of existing projects in the portfolio
                        portfolioManager.ShowProjects();
                        break;
                    case 3:
                        // Option 3: Select a project from the list for further actions
                        portfolioManager.SelectProject();
                        break;
                    case 4:
                        // Option 4: Add a transaction (sale or purchase) to the selected project
                        portfolioManager.AddTransaction();
                        break;
                    case 5:
                        // Option 5: Remove the selected project from the portfolio
                        portfolioManager.RemoveProject();
                        break;
                    case 6:
                        // Option 6: Display all sales transactions for the selected project
                        portfolioManager.DisplaySalesTransactions();
                        break;
                    case 7:
                        // Option 7: Display all purchase transactions for the selected project
                        portfolioManager.DisplayPurchaseTransactions();
                        break;
                    case 8:
                        // Option 8: Display a summary of total sales and purchases for the selected project
                        portfolioManager.DisplayProjectSummary();
                        break;
                    case 9:
                        // Option 9: Display a summary of total sales and purchases for the entire portfolio
                        portfolioManager.DisplayPortfolioSummary();
                        break;
                    case 10:
                        // Option 10: Load data from Beige software report to populate the portfolio
                        portfolioManager.LoadData();
                        break;
                    case 0:
                        // Option 0: Exit the portfolio management system
                        running = false;
                        break;
                    default:
                        // Handle any invalid menu option input
                        Console.WriteLine("Invalid option");
                        break;
                }

                Console.WriteLine();
            }
        }
```

### Adding a Project
The AddProject method prompts the user to input the project's name and type, and then creates and adds the corresponding project object to the list of projects.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/4e2603a4-3844-4932-820a-b77d7c9c56a3)

### Displaying Projects
The ShowProjects method lists all the existing projects in the portfolio, allowing easy project selection for further actions.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/07e78414-2e16-46a3-894d-74c0c5317b6e)

### Selecting a Project
The SelectProject method lets the user choose a project from the list, which becomes the currently selected project for subsequent operations.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/107bc1aa-c963-4d0f-8885-a52d6623f985)

### Adding a Transaction
The AddTransaction method allows the user to add a transaction (either a sale or purchase) to the selected project, updating the project's transaction list accordingly.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/9b06aaf8-9e85-4060-8de8-90da50f9349b)

### Removing a Project
With the RemoveProject method, the user can remove the currently selected project from the portfolio, clearing the way for further operations.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/10f5b7f6-cf27-430d-a57d-b6de16414363)

### Displaying Transactions
The DisplaySalesTransactions and DisplayPurchaseTransactions methods display all sales and purchase transactions, respectively, for the selected project.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/ae51d52f-4fc4-4925-9efb-c279dc433076)

### Displaying Project Summary
The DisplayProjectSummary method displays a summary of the selected project, providing details about sales, purchases, profits, and, in the case of NewBuildProject, the expected tax refund.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/79882693-90c1-4b64-b08b-925a470dd26a)

### Displaying Portfolio Summary
The DisplayPortfolioSummary method gives an overview of the entire portfolio, calculating the total sales, purchases, and profits, as well as the total tax refundable amount for NewBuildProject projects.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/5713fe8f-5b08-4db5-a64b-889bbbb43b0f)

### Loading Data
The LoadData method enables loading data from a file, parsing it, and populating the project's transaction lists. This feature offers a convenient way to import transaction data into the portfolio.

All of the functionality above works and has been tested thoroughly, however the loading data section, unfortunately, I could not fully get to work. But, you can see below how far I did get in the given timeframe.

![image](https://github.com/University-of-Hull-CST/441101-2223-capstone-project-samuelbslawrence/assets/114992120/527a54e3-dd28-4f86-b1fb-7b43d212b653)

## Conclusion
My code illustrates a well-designed portfolio management system, that successfully isolates tasks through abstract classes, provides data encapsulation, prevents code duplication through inheritance, and makes use of runtime polymorphism for dynamic behaviour. In addition to improving code organisation and maintainability, these capabilities offer a robust and user-friendly framework for managing numerous projects and transactions.
