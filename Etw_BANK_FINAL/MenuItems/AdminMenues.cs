﻿using Etw_BANK_FINAL.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.MenuItems
{
    internal class AdminMenues:Method
    {

        // Define an AdminMenu method
        public static void AdminMenu()
        {
           

            int choice;

            do
            {
                // Clear the console and display a welcome message for the current user
                Console.Clear();
                Console.WriteLine($"\n\n\t\t\t  =====================WELCOME \u001b[32m {currentUser.UserName.ToUpper()} \u001b[0m TO ETW - BANK========================\n\n");

                Console.WriteLine("\n\n\t\t\t\t\t\t What do you wish to do");
                Console.WriteLine("\t\t\t\t\t\t ======================\n\n");


                Console.WriteLine($"\t\t\t\t\u001b[35m {"Enter Choice 1-3"}\u001b[0m");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\n\n");
                Console.WriteLine($"\t\t\t\t{"1. Add New User"}");
                Console.WriteLine($"\t\t\t\t{"2. View All Customers"}");
                Console.WriteLine($"\t\t\t\t{"3. Logout"}");

                // Read user's choice
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                     Console.WriteLine("\n\t\t\t\t \u001b[31m Invalid input . Please enter a 1-3.\u001b[0m \n");

                    Thread.Sleep(2000);
                    Console.Clear();

                    continue;
                }

                // Perform actions based on user's choice
                switch (choice)
                {
                    case 1:
                        Method.NewUser();
                        break;

                    case 2:
                        Method.ViewAllUsers();
                        break;

                    case 3:
                        Method.Logout();
                        break;

     
                }

            } while (choice != 3);
        }
    }
}
