using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniProjectCRM
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Choose menu option: \n 1. Add new client \n 2. Show all clients");
            string option = Console.ReadLine();
            int num = Convert.ToInt32(option);
            switch (num)
            {
                case 1:
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
                    break;

                case 2:
                    StreamReader sr = new StreamReader("./clients.csv");
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Console.WriteLine(line);
                    }
                    sr.Close();
                    break;
                case 3:

                    break;
                default:
                    Console.WriteLine("Invalid number.");
                    break;
            }


            Console.WriteLine("\n\n\n");
            Console.WriteLine("Enter any key for finish this program");
            Console.ReadLine();
        }
    }
}
