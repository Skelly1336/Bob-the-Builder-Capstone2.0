namespace PortfolioManagement

{
    /// <summary>
    /// Define an enumeration called TransactionType to represent different types of transactions
    /// </summary>
    public enum TransactionType
    {
        None = -1,
        Sale = 1,       // Enum value representing a sale transaction
        Purchase = 2    // Enum value representing a purchase transaction
    }

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
}
