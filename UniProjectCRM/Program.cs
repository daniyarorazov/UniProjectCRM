
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace UniProjectCRM
{

    class MenuActions
    {
        public string IfIsNullValue(string value, string nameField)
        {
            while (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Invalid input. Please enter a non-empty [{0}]:", nameField);
                value = Console.ReadLine();
                

            }
            return value;
        }

        public string DateValidation(string value)
        {
            DateTime date;

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
        public void AddClient()
        {
            StreamWriter sw = new StreamWriter("./clients.csv", true);
            bool state = true;
            while (state)
            {
                Console.WriteLine("Name of client");
                string name = Console.ReadLine();
                name = IfIsNullValue(name, "Name of client");

                Console.WriteLine("Surname of client");
                string surname = Console.ReadLine();
                surname = IfIsNullValue(surname, "Surname of client");

                Console.WriteLine("Date of Birth client (example: 24.02.2022)");
                string dateBirth = Console.ReadLine();
                dateBirth = IfIsNullValue(dateBirth, "Date of Birth client (example: 24.02.2022)");
                dateBirth = DateValidation(dateBirth);

                Console.WriteLine("Sum of all purchases ($)");
                string SumAllPurchases = Console.ReadLine();
                SumAllPurchases = IfIsNullValue(SumAllPurchases, "Sum of all purchases ($)");

                decimal sum;

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
        public void ShowAllClients()
        {
            // Get the lines of the CSV file
            string[] lines = File.ReadAllLines("./clients.csv");

            // Create the header line
            string header = "║" + " Name".PadRight(22) + "║" + " Surname".PadRight(21) + "║" + " Date of birth".PadRight(16) + "║" + " Sum of payments".PadRight(19) + "║" + " Last interaction".PadRight(25) + "║";

            // Calculate the length of the header line
            int headerLength = header.Length; // Add 2 for the padding on the sides

            // Create the horizontal line
            string horizontalLine = "╠" + "═".PadRight(headerLength - 2, '═') + "╣";

            // Create the footer line
            string footer = "╚" + "═".PadRight(headerLength - 2, '═') + "╝";

            // Print the header line
            Console.WriteLine("╔" + "═".PadRight(headerLength - 2, '═') + "╗");
            Console.WriteLine(header);
            Console.WriteLine(horizontalLine);

            // Print the data lines
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string name = " " + data[0].Trim().PadRight(20);
                string surname = " " + data[1].Trim().PadRight(20);
                string dateOfBirth = " " + data[2].Trim().PadRight(15);
                string sumOfPayments = " " + data[3].Trim().PadRight(18);
                string lastInteraction = " " + data[4].Trim().PadRight(24);

                // Create the data line
                string dataLine = "║" + (i+1) + name + "║" + surname + "║" + dateOfBirth + "║" + sumOfPayments + "║" + lastInteraction + "║";

                // Print the data line
                Console.WriteLine(dataLine);

                // Print the horizontal line if this is not the last line
                if (i < lines.Length - 1)
                {
                    Console.WriteLine(horizontalLine);
                }
            }

            // Print the footer line
            Console.WriteLine(footer);
           
        }
        public void EditDataClient()
        {

            ShowAllClients();
            string[] lines = File.ReadAllLines("./clients.csv");
            Console.Write("Choose the number of the client to edit: ");
            string indexNum = Console.ReadLine();
            decimal num;

            while (!decimal.TryParse(indexNum, out num))
            {
                Console.WriteLine("Invalid input. Please enter a valid number");
                indexNum = Console.ReadLine();
            }

            int index = int.Parse(indexNum) - 1;
            if (index < 0 || index >= lines.Length)
            {
                Console.WriteLine("Invalid client number.");
                return;
            }

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
        public void DeleteClient()
        {
            string[] lines = File.ReadAllLines("./clients.csv");
            bool state = true;

            while (state)
            {
                ShowAllClients();
                Console.Write("Enter the ID of the client you want to delete: ");
                int id = int.Parse(Console.ReadLine());

                // Remove the selected row from the list of rows
                lines = lines.Where((line, index) => index != id - 1).ToArray();

                // Write the updated list of rows back to the CSV file
                using (var writer = new StreamWriter("clients.csv"))
                {
                    foreach (var row in lines)
                    {
                        writer.WriteLine(row);
                    }
                }

                Console.WriteLine("Client with ID {0} has been deleted.", id);

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
                "5. Exit ");
                string option = Console.ReadLine();
                int num;

                if (!int.TryParse(option, out num) || num > 5)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                    
                }
                if (num == 5)
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
                    default:
                        Console.WriteLine("Invalid number.");
                        break;
                }
            }
        }
    }
}
