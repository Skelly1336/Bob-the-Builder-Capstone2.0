using System;
using System.Collections.Generic;
using System.IO;

namespace PortfolioManagement

{
    class Program
    {
        /// <summary>
        /// Create an instance of the PortfolioManager class to manage the portfolio and its projects
        /// </summary>
        /// <param name="args"></param>
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
    }
}
