using Etw_BANK_FINAL.Logins;
using Etw_BANK_FINAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.WelcomeUI
{
    internal class Welcome
    {

        // Method to display the welcome screen and navigate to different login options
        public static void WelcomeUI()
        {
            // Displaying the welcome message and instructions
            Console.WriteLine($"\n\n\t\t\t  =====================WELCOME TO ETW - BANK========================\n\n");
            Console.WriteLine("\n\t\t\t\tPlease use \u001b[34m UP \u001b[0m arrow To move up and \u001b[34m Down \u001b[0m to Move Down\n\n");

            // Initializing variables for navigation
            ConsoleKeyInfo key;
            int choice = 1;
            bool isChoosen = false;
            (int left, int top) = Console.GetCursorPosition();

            string color = "\u001b[32m";

            // Loop to navigate through options until choice is made
            while (!isChoosen)
            {
                Console.SetCursorPosition(left, top);

                // Displaying administrator and customer options
                Console.WriteLine($"\n\t\t\t\t\t\t   {(choice == 1 ? color : "")}Administrator\u001b[0m\n");
                Console.WriteLine($"\n\t\t\t\t\t\t    {(choice == 2 ? color : "")}Customer\u001b[0m\n");
                Console.WriteLine($"\n\n\t\t\t\t\t     Hit \u001b[32m ENTER \u001b[0m to Continue  \n");

                key = Console.ReadKey(true);

                // Handling arrow key input for navigation
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (choice == 2)
                            choice = 1;
                        else
                            choice++;
                        break;

                    case ConsoleKey.UpArrow:
                        if (choice == 1)
                            choice = 2;
                        else
                            choice--;
                        break;

                    case ConsoleKey.Enter:
                        Console.WriteLine("\t\t\n");
                        Utility.Loading();
                        isChoosen = true;

                        // Navigating based on the selected choice
                        if (choice == 1)
                        {
                            Console.Clear();
                            Login.AdminLogin();
                        }
                        else if (choice == 2)
                        {
                            Console.Clear();
                            Login.UserLogin();
                        }
                        break;
                }
            }
        }

    }
}
