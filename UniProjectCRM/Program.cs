using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UniProjectCRM
{

    // Class Menu with all actions
    class MenuActions
    {
        // Validation. If entering value from user is null, "", so will display a message for the user to enter the non empty value
        public string IfIsNullValue(string value, string nameField)
        {
            while (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Invalid input. Please enter a non-empty [{0}]:", nameField);
                value = Console.ReadLine();
            }
            return value;
        }

        // Validation for date format
        public string DateValidation(string value)
        {
            DateTime date;

            // Checking if entering date format is dd.MM.yyyy and the date is no larger than today's date
            while (!DateTime.TryParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) || date > DateTime.Today)
            {
                if (date > DateTime.Today)
                {
                    Console.WriteLine("Invalid input. The date cannot be later than today's date.");
                    value = Console.ReadLine();
                } else
                {
                    Console.WriteLine("Invalid input. Please enter a date in the format dd.MM.yyyy:");
                    value = Console.ReadLine();
                }
               
            }
            return value;
        }

        // Adding new client
        public void AddClient()
        {
            StreamWriter sw = new StreamWriter("./clients.csv", true);
            bool state = true;
            while (state)
            {
                Console.WriteLine("Name of client");
                string name = Console.ReadLine();
                name = IfIsNullValue(name, "Name of client");
                Console.WriteLine();

                Console.WriteLine("Surname of client");
                string surname = Console.ReadLine();
                surname = IfIsNullValue(surname, "Surname of client");
                Console.WriteLine();

                Console.WriteLine("Date of Birth client (example: 21.02.2004)");
                string dateBirth = Console.ReadLine();
                dateBirth = IfIsNullValue(dateBirth, "Date of Birth client (example: 24.02.2022)");
                dateBirth = DateValidation(dateBirth);
                Console.WriteLine();

                Console.WriteLine("Sum of all purchases ($)");
                string SumAllPurchases = Console.ReadLine();
                SumAllPurchases = IfIsNullValue(SumAllPurchases, "Sum of all purchases ($)");
                Console.WriteLine();

                decimal sum;

                // Validation for entering only numeric values
                while (!decimal.TryParse(SumAllPurchases, out sum))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for the sum of all purchases:");
                    SumAllPurchases = Console.ReadLine();
                }



                Console.WriteLine("Last contact with him (example: 24.02.2022)");
                string lastActivityWithClient = Console.ReadLine();
                lastActivityWithClient = IfIsNullValue(lastActivityWithClient, "Last contact with him (example: 24.02.2022)");
                lastActivityWithClient = DateValidation(lastActivityWithClient);

                string output = string.Format("{0}, {1}, {2}, {3}$, {4}", name, surname, dateBirth, SumAllPurchases, lastActivityWithClient);
                sw.WriteLine(output);


                Console.WriteLine("Do you want to add another user? \n 1. Yes \n 2. No");
                string answerForAddingAnother = Console.ReadLine();
                if (answerForAddingAnother == "2")
                {
                    state = false;
                    break;
                }
                if (answerForAddingAnother == "1") {
                    state = true;
                    Console.Clear();
                }
                while (answerForAddingAnother != "1" && answerForAddingAnother != "2")
                {
                    Console.WriteLine("Do you want to add another user? \n 1. Yes \n 2. No");
                    answerForAddingAnother = Console.ReadLine();
                    if (answerForAddingAnother == "2")
                    {
                        state = false;
                        break;
                    }
                    if (answerForAddingAnother == "1")
                    {
                        state = true;
                        break;
                    }
                }
                if (!state)
                {
                    break;
                }

            }
            sw.Close();
        }

        // Show all clients data
        public void ShowAllClients()
        {
            string[] lines = File.ReadAllLines("./clients.csv");

            // Creating data table structure
            string header = "║" + " Name".PadRight(22) + "║" + " Surname".PadRight(21) + "║" + " Date of birth".PadRight(16) + "║" + " Sum of payments".PadRight(19) + "║" + " Last interaction".PadRight(25) + "║";
            int headerLength = header.Length; 
            string horizontalLine = "╠" + "═".PadRight(headerLength - 2, '═') + "╣";
            string footer = "╚" + "═".PadRight(headerLength - 2, '═') + "╝";
            Console.WriteLine("╔" + "═".PadRight(headerLength - 2, '═') + "╗");
            Console.WriteLine(header);
            Console.WriteLine(horizontalLine);

            // Paddings inside table columns
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string name = " " + data[0].Trim().PadRight(20);
                string surname = " " + data[1].Trim().PadRight(20);
                string dateOfBirth = " " + data[2].Trim().PadRight(15);
                string sumOfPayments = " " + data[3].Trim().PadRight(18);
                string lastInteraction = " " + data[4].Trim().PadRight(24);

                string dataLine = "║" + (i+1) + name + "║" + surname + "║" + dateOfBirth + "║" + sumOfPayments + "║" + lastInteraction + "║";

                Console.WriteLine(dataLine);

                if (i < lines.Length - 1)
                {
                    Console.WriteLine(horizontalLine);
                }
            }

            Console.WriteLine(footer);
        }

        // Edit data clients
        public void EditDataClient()
        {
            ShowAllClients();
            string[] lines = File.ReadAllLines("./clients.csv");
            Console.Write("Choose the number of the client to edit (if you want back to menu, enter 0): ");
            string indexNum = Console.ReadLine();
            decimal num;

            // Validation for entering only numeric values
            while (!decimal.TryParse(indexNum, out num) || num < 1 || num > lines.Length)
            {
                // If user enter 0, program back to menu
                if (num == 0)
                {
                    return;
                }
                Console.WriteLine("Invalid input. Please enter a valid client number between 1 and {0}", lines.Length);
                indexNum = Console.ReadLine();
            }

            int index = int.Parse(indexNum) - 1;


            // Update the chosen client's data
            string[] chosenFields = lines[index].Split(',');
            Console.Write("Enter new date of birth: ");
            string newDob = Console.ReadLine();
            newDob = IfIsNullValue(newDob, "Date of birth");
            newDob = DateValidation(newDob);

            Console.Write("Enter new sum of payments: ");
            string newPayments = Console.ReadLine();
            newPayments = IfIsNullValue(newPayments, "Date of birth");
            decimal sum;

            // Validation for entering only numeric values
            while (!decimal.TryParse(newPayments, out sum))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the sum of all purchases:");
                newPayments = Console.ReadLine();
            }

            Console.Write("Enter new last interaction date: ");
            string newInteraction = Console.ReadLine();
            newInteraction = IfIsNullValue(newInteraction, "Last interaction");
            newInteraction = DateValidation(newInteraction);

            chosenFields[2] = newDob;
            chosenFields[3] = (newPayments + "$");
            chosenFields[4] = newInteraction;
            lines[index] = string.Join(",", chosenFields);

            // Write the updated data back to the CSV file
            File.WriteAllLines("./clients.csv", lines);
            Console.WriteLine("Data updated successfully.");
        }

        // Delete client
        public void DeleteClient()
        {
            string[] lines = File.ReadAllLines("./clients.csv");
            bool state = true;

            while (state)
            {
                ShowAllClients();
                Console.Write("Enter the ID of the client you want to delete (If you want back to menu, enter 0): ");
                string indexNum = Console.ReadLine();
                if (indexNum == "0")
                {
                    return;
                }
                decimal num;

                // Validation for entering only numeric values
                while (!decimal.TryParse(indexNum, out num))
                {
                    if (num == 0)
                    {
                        return;
                    }
                    Console.WriteLine("Invalid input. Please enter a valid number");
                    indexNum = Console.ReadLine();
                }

                int id = int.Parse(indexNum) - 1;

                int maxId = lines.Length - 1;

                while (id < 0 || id > maxId)
                {
                    
                    Console.WriteLine($"Please enter a number from 1 to {maxId + 1}");
                    indexNum = Console.ReadLine();
                    if (indexNum == "0")
                    {
                        return;
                    }

                    // Validation for entering only numeric values
                    while (!decimal.TryParse(indexNum, out num))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number");
                        indexNum = Console.ReadLine();
                        if (indexNum == "0")
                        {
                            return;
                        }
                    }
                    id = int.Parse(indexNum) - 1;
                }


                // Remove the selected row from the list of rows
                lines = lines.Where((line, index) => index != id).ToArray();

                // Write the updated list of rows back to the CSV file
                using (var writer = new StreamWriter("clients.csv"))
                {
                    foreach (var row in lines)
                    {
                        writer.WriteLine(row);
                    }
                }

                Console.WriteLine("Client with ID {0} has been deleted.", id+1);

                // Condition for adding another user
                Console.WriteLine("Do you want to delete another user? \n 1. Yes \n 2. No");
                string answerForAddingAnother = Console.ReadLine();
                if (answerForAddingAnother == "2")
                {
                    state = false;
                    break;
                }
                if (answerForAddingAnother == "1")
                {
                    state = true;
                    Console.Clear();
                }
                while (answerForAddingAnother != "1" && answerForAddingAnother != "2")
                {
                    Console.WriteLine("Do you want to delete another user? \n 1. Yes \n 2. No");
                    answerForAddingAnother = Console.ReadLine();
                    if (answerForAddingAnother == "2")
                    {
                        state = false;
                        break;
                    }
                    if (answerForAddingAnother == "1")
                    {
                        state = true;
                        Console.Clear();

                        break;
                    }
                }
                if (!state)
                {
                    break;
                }
            }
        }

        // Show Statistic
        public void ShowStats()
        {
            string[] lines = File.ReadAllLines("./clients.csv");

            // Sum of all transactions
            decimal totalTransactionSum = 0;
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                if (data.Length >= 4 && decimal.TryParse(data[3].Replace("$", ""), out decimal transactionSum))
                {
                    totalTransactionSum += transactionSum;
                }
            }
            Console.WriteLine($"Sum of all transactions: {totalTransactionSum}$");

            // Date of last transaction and name of client
            DateTime lastTransactionDate = DateTime.MinValue;
            string lastTransactionClientName = "";
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                if (data.Length >= 5 && DateTime.TryParse(data[4], out DateTime interactionDate))
                {
                    if (interactionDate > lastTransactionDate)
                    {
                        lastTransactionDate = interactionDate;
                        lastTransactionClientName = $"{data[0]} {data[1]}";
                    }
                }
            }
            Console.WriteLine($"Last transaction was made by {lastTransactionClientName} on {lastTransactionDate}");

            // Bigger sum of transaction
            decimal maxTransactionSum = decimal.MinValue;
            string maxTransactionClientName = "";
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                if (data.Length >= 4 && decimal.TryParse(data[3].Replace("$", ""), out decimal transactionSum))
                {
                    if (transactionSum > maxTransactionSum)
                    {
                        maxTransactionSum = transactionSum;
                        maxTransactionClientName = $"{data[0]} {data[1]}";
                    }
                }
            }
            Console.WriteLine($"Client with the biggest transaction sum is {maxTransactionClientName} with sum of {maxTransactionSum}$");

            Console.WriteLine();
            Console.WriteLine("For back to menu, click Enter");
            Console.ReadLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            MenuActions menuActions = new MenuActions();

            while (true)
            {
                Console.WriteLine("Choose menu option: \n " +
                "1. Add new client \n " +
                "2. Show all clients \n " +
                "3. Edit data of client \n " +
                "4. Delete client \n " +
                "5. Show statistics \n " +
                "6. Exit ");
                string option = Console.ReadLine();
                int num;

                // Validation for numbers between 1 and 5
                if (!int.TryParse(option, out num) || num > 5)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                    
                }
                if (num == 6)
                {
                    break;
                }
                switch (num)
                {
                    case 1:
                        Console.Clear();
                        menuActions.AddClient();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        menuActions.ShowAllClients();
                        Console.WriteLine("Enter any value for continue ->");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        menuActions.EditDataClient();
                        Console.WriteLine("Enter any value for continue ->");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        menuActions.DeleteClient();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        menuActions.ShowStats();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid number.");
                        break;
                }
            }
        }
    }
}
