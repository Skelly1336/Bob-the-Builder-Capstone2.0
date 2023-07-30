using System;
using System.Collections.Generic;
using System.IO;

namespace PortfolioManagement

{
    /// <summary>
    /// The PortfolioManager class is responsible for managing a list of projects and their transactions.
    /// </summary>
    class PortfolioManager
    {
        private List<Project> projects; // List to store Project objects.
        private int selectedProjectIndex; // The index of the currently selected project.

        public PortfolioManager()
        {
            projects = new List<Project>(); // Initialize an empty list of projects.
            selectedProjectIndex = -1; // Initialize the selected project index as -1 (no project selected).
        }


        /// <summary>
        /// Method to add a new project to the list.
        /// </summary>
        public void AddProject()
        {
            Console.Write("Enter project name: ");
            string name = Console.ReadLine(); // Read the project name from the user.

            Console.Write("Enter project type (1 for new build, 2 for renovation): ");
            ProjectType type = (ProjectType)int.Parse(Console.ReadLine()); // Read the project type as an integer and convert it to an enum.

            Project project; // Declare a Project object to hold the new project.

            // Create the appropriate type of project based on the user's input.
            if (type == ProjectType.Build)
            {
                project = new NewBuildProject(name); // Create a new build project.
            }
            else
            {
                project = new RenovationProject(name); // Create a renovation project.
            }

            projects.Add(project); // Add the new project to the list.
            Console.WriteLine("Project added");
        }

        /// <summary>
        /// Method to display a list of all projects and their indices.
        /// </summary>
        public void ShowProjects()
        {
            // Method to display a list of all projects and their indices.
            Console.WriteLine("List of projects:");
            for (int i = 0; i < projects.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, projects[i].Name); // Display each project's index and name.
            }
        }

        /// <summary>
        /// Method to select a project from the list.
        /// </summary>
        public void SelectProject()
        {
            ShowProjects(); // Show the list of projects first.
            Console.Write("Enter project number: ");
            int projectIndex = int.Parse(Console.ReadLine()) - 1; // Read the project index from the user (subtracting 1 to match the zero-based index).

            // Check if the entered project index is valid.
            if (projectIndex < 0 || projectIndex >= projects.Count)
            {
                Console.WriteLine("Invalid project number");
            }
            else
            {
                selectedProjectIndex = projectIndex; // Set the selected project index to the user's choice.
                Console.WriteLine("Project selected: {0}", projects[selectedProjectIndex].Name); // Display the selected project's name.
            }
        }

        /// <summary>
        /// Method to add a transaction to the selected project.
        /// </summary>
        public void AddTransaction()
        {
            if (selectedProjectIndex == -1)
            {
                Console.WriteLine("No project selected");
                return;
            }

            Console.Write("Enter transaction type (1 for sale, 2 for purchase): ");
            TransactionType type = (TransactionType)int.Parse(Console.ReadLine()); // Read the transaction type from the user and convert it to an enum.

            Console.Write("Enter transaction amount: ");
            decimal amount = decimal.Parse(Console.ReadLine()); // Read the transaction amount from the user.

            Console.Write("Enter transaction description: ");
            string description = Console.ReadLine(); // Read the transaction description from the user.

            Transaction transaction; // Declare a Transaction object to hold the new transaction.

            transaction = new Transaction(amount, description, type); // Create a new transaction with the provided data.

            projects[selectedProjectIndex].AddTransaction(transaction); // Add the transaction to the selected project's list of transactions.
        }

        /// <summary>
        /// Method to remove the selected project from the list.
        /// </summary>
        public void RemoveProject()
        {
            if (selectedProjectIndex == -1)
            {
                Console.WriteLine("No project selected");
                return;
            }

            projects.RemoveAt(selectedProjectIndex); // Remove the selected project from the list.
            selectedProjectIndex = -1; // Reset the selected project index since there's no project selected anymore.
            Console.WriteLine("Project removed");
        }

        /// <summary>
        /// Method to display all sales transactions for the selected project.
        /// </summary>
        public void DisplaySalesTransactions()
        {
            if (selectedProjectIndex == -1)
            {
                Console.WriteLine("No project selected");
                return;
            }

            projects[selectedProjectIndex].DisplayTransactions(transaction => transaction.Type is TransactionType.Sale); // Call the DisplayTransactions method of the selected project, filtering only sales transactions.
        }

        /// <summary>
        /// Method to display all purchase transactions for the selected project.
        /// </summary>
        public void DisplayPurchaseTransactions()
        {
            if (selectedProjectIndex == -1)
            {
                Console.WriteLine("No project selected");
                return;
            }

            projects[selectedProjectIndex].DisplayTransactions(transaction => transaction.Type is TransactionType.Purchase); // Call the DisplayTransactions method of the selected project, filtering only purchase transactions.
        }

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

        /// <summary>
        /// Method to load data from a file and populate the projects' transactions.
        /// </summary>
        public void LoadData()
        {
            Console.Write("Enter file path: ");
            string filePath = Console.ReadLine(); // Read the file path from the user.

            try
            {
                string[] lines = File.ReadAllLines(filePath); // Read all lines from the specified file.

                // Loop through each line and parse the data to create transactions and projects.
                foreach (string line in lines)
                {
                    string[] data = line.Split(','); // Split the line by comma to get individual data elements.

                    if (data.Length < 4) // Check if the line contains enough data.
                    {
                        Console.WriteLine("Invalid data format: " + line);
                        continue; // Skip this line and move to the next one.
                    }

                    string projectName = data[0];
                    ProjectType projectType = ProjectType.None;
                    TransactionType transactionType = TransactionType.None;

                    switch (data[1])
                    {
                        case "L":
                            projectType = ProjectType.Build;
                            break;
                        case "R":
                            projectType = ProjectType.Renovation;
                            break;
                        default:
                            Console.WriteLine("Invalid project type: " + data[1]);
                            continue; // Skip this line and move to the next one.
                    }

                    switch (data[2])
                    {
                        case "S":
                            transactionType = TransactionType.Sale;
                            break;
                        case "P":
                            transactionType = TransactionType.Purchase;
                            break;
                        default:
                            Console.WriteLine("Invalid transaction type: " + data[2]);
                            continue; // Skip this line and move to the next one.
                    }

                    decimal amount = decimal.Parse(data[3]);
                    string description = data[4];

                    // Find the project with the specified name in the list of projects.
                    Project project = projects.Find(p => p.Name == projectName);

                    // If the project doesn't exist, create a new one based on the project type and add it to the list.
                    if (project == null)
                    {
                        if (projectType == ProjectType.Build)
                        {
                            project = new NewBuildProject(projectName); // Create a new build project.
                        }
                        else
                        {
                            project = new RenovationProject(projectName); // Create a renovation project.
                        }

                        projects.Add(project); // Add the new project to the list.
                    }

                    Transaction transaction = new Transaction(amount, description, transactionType); // Create a new transaction with the provided data.
                    project.AddTransaction(transaction); // Add the transaction to the project's list of transactions.
                }

                Console.WriteLine("Data loaded from file");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid file format");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading data: " + ex.Message);
            }
        }
    }
}
