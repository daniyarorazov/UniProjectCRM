
using System;

using System.IO;


namespace UniProjectCRM
{

    class MenuActions
    {
        public void AddClient()
        {
            StreamWriter sw = new StreamWriter("./clients.csv", true);

            while (true)
            {
                Console.WriteLine("Name of client");
                string name = Console.ReadLine();
                Console.WriteLine("Surname of client");
                string surname = Console.ReadLine();
                Console.WriteLine("Date of Birth client (24/02/2022)");
                string dateBirth = Console.ReadLine();
                Console.WriteLine("Sum of all purchases ($)");
                string SumAllPurchases = Console.ReadLine();
                Console.WriteLine("Last contact with him (24/02/2022)");
                string lastActivityWithClient = Console.ReadLine();
                string output = string.Format("{0}, {1}, {2}, {3}$, {4}", name, surname, dateBirth, SumAllPurchases, lastActivityWithClient);
                sw.WriteLine(output);


                Console.WriteLine("Do you want to add another user? \n 1. Yes \n 2. No");
                string answerForAddingAnother = Console.ReadLine();
                if (answerForAddingAnother == "2")
                {
                    break;
                }
            }
            sw.Close();
        }
        public void ShowAllClients()
        {// Get the lines of the CSV file
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
            int index = int.Parse(Console.ReadLine()) - 1;
            if (index < 0 || index >= lines.Length)
            {
                Console.WriteLine("Invalid client number.");
                return;
            }

            // Update the chosen client's data
            string[] chosenFields = lines[index].Split(',');
            Console.Write("Enter new date of birth: ");
            string newDob = Console.ReadLine();
            Console.Write("Enter new sum of payments: ");
            string newPayments = Console.ReadLine();
            Console.Write("Enter new last interaction date: ");
            string newInteraction = Console.ReadLine();
            chosenFields[2] = newDob;
            chosenFields[3] = newPayments;
            chosenFields[4] = newInteraction;
            lines[index] = string.Join(",", chosenFields);

            // Write the updated data back to the CSV file
            File.WriteAllLines("./clients.csv", lines);
            Console.WriteLine("Data updated successfully.");
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
                "4. Exit ");
                string option = Console.ReadLine();
                int num = Convert.ToInt32(option);
                if (num == 4)
                {
                    break;
                }
                switch (num)
                {
                    case 1:
                        menuActions.AddClient();
                        Console.Clear();
                        break;

                    case 2:
                        menuActions.ShowAllClients();
                        Console.WriteLine("Enter any value for continue ->");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        menuActions.EditDataClient();
                        Console.WriteLine("Enter any value for continue ->");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid number.");
                        break;
                }
            }


        }
    }
}
