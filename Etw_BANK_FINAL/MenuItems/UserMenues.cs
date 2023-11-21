using Etw_BANK_FINAL.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.MenuItems
{
    internal class UserMenues:Method
    {

        // Define a UserMenu method
        public static void UserMenu()
        {
           

            int choice;
            do
            {
                // Clear the console and display a welcome message for the current user
                Console.Clear();
                Console.WriteLine($"\n\n\t\t\t  =====================WELCOME \u001b[32m {currentUser.UserName.ToUpper()} \u001b[0m TO ETW - BANK========================\n\n\n");

                Console.WriteLine("\n\n\t\t\t\t\t\t What do you wish to do");
                Console.WriteLine("\t\t\t\t\t\t ======================\n\n");

                Console.WriteLine($"\t\t\t\t\u001b[35m {"Enter Choice 1-6"}\u001b[0m");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\n\n");
                Console.WriteLine($"\t\t\t\t{"1. View Balance"}");
                Console.WriteLine($"\t\t\t\t{"2. Transfer Money"}");
                Console.WriteLine($"\t\t\t\t{"3. Withdraw Money"}");
                Console.WriteLine($"\t\t\t\t{"4. Deposit Money"}");
                Console.WriteLine($"\t\t\t\t{"5. Create New Account"}");
                Console.WriteLine($"\t\t\t\t{"6. Logout"}");
                Console.ResetColor();

                // Read the user's input and perform actions based on their choice
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
                {

                    Console.WriteLine("\n\t\t\t\t \u001b[31m Invalid input . Please enter a 1-6.\u001b[0m \n");

                    Thread.Sleep(2000);
                    Console.Clear();
                    continue;
                }

                    switch (choice)
                    {
                        case 1:
                            Method.ViewBalance();
                            break;
                        case 2:
                            Method.TransferMoney();
                            break;
                        case 3:
                            Method.WithdrawMoney();

                            break;
                        case 4:
                            Method.DepositMoney();
                            break;
                        case 5:
                            Method.CreateBankAccount(currentUser);
                            break;
                        case 6:
                            Method.Logout();
                            break;
                   
                    }
              
                    Console.WriteLine("\n\t\t\t\t \u001b[31m Invalid input . Please enter a 1-6.\u001b[0m \n");
                

            } while (choice != 6);
        }
    }
}
