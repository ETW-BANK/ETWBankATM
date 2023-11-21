using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.Utilities
{
    internal class Utility1
    {
        // Utility class containing various utility methods

        // Method to animate loading dots
        public static void AnimateLoadingDots(int numberOfDots, int animationSpeed)
        {
            // Code to animate loading dots and control cursor visibility

            Console.CursorVisible = false;

            for (int i = 0; i < numberOfDots; i++)
            {
                Console.Write(".");
                Thread.Sleep(animationSpeed);
            }

            Console.CursorVisible = true;
        }

        // Method to display a loading message while animating dots
        public static void Loading()
        {

            Console.Write("\t\t\t\t\t \u001b[33m Loading \u001b[0m...");

            // Code to display loading message and call the AnimateLoadingDots method

            int numberOfDots = 20;
            int animationSpeed = 200;

            AnimateLoadingDots(numberOfDots, animationSpeed);

        }

        // Method to hide user input for PIN entry
        public static SecureString PinHide()
        {
            // Code to hide PIN entry as asterisks

            Console.WriteLine("\n\n\t\t\t\t\t\t  \u001b[33m Enter Your PIN\u001b[0m\n");
            SecureString Pin = new SecureString();
            ConsoleKeyInfo keyInfo;
            Console.Write("\n\n\t\t\t\t\t\t\t");
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Backspace && Pin.Length > 0)

                {
                    Pin.RemoveAt(Pin.Length - 1);
                    Console.Write("\b \b");
                }

                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Pin.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            {
                return Pin;
            }
        }

        // Method to generate a random account number
        public static int GenerateAccNumber()

        {
            // Code to generate a random account number of a specified size
            Random random = new Random();
            int Accnumber=0;


            int size = 6;
            for (int i = 0; i < size; i++)
            {
                int rand = random.Next(0, 9);
                Accnumber =Accnumber*10 + rand;
            }

            return Accnumber;
        }
        // Method to generate a random PIN
        public static string GeneratePin()

        {
            // Code to generate a random PIN of a specified size
            Random random = new Random();
            string pinnumber = " ";

            int size = 4;

            for (int i = 0; i < size; i++)
            {
                int rand = random.Next(1000, 9999);

                pinnumber = rand.ToString();


            }

            return pinnumber;
        }


        // Method to prompt user for currency input and validate
        public static string CheckCurrency()
        {
            string CurrencyType;

            do
            {
                // Code to prompt for and validate currency input
                Console.Write("Enter Currency:");
                CurrencyType = Console.ReadLine().ToUpper();


                if (CurrencyType != "USD" && CurrencyType != "EURO" && CurrencyType != "SEK")
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("\n\u001b[31mInvalid Currency please enter (USD/EURO/SEK)\u001b[0m\n");

                    Console.ResetColor();
                }
                else
                {
                    return CurrencyType;
                }

            } while (true);


        }

        //Method to perform currency conversion

        public static decimal CurrencyChanger(string val1, string val2, decimal amount)
        {
            // Code to perform currency conversion based on input values

            decimal exchangeRate = 1.0m; // Default exchange rate if no conversion is needed

            if (val1 == "USD" && val2 == "SEK")
            {
                exchangeRate = exchangeRate * 10;
            }
            else if (val1 == "SEK" && val2 == "USD")
            {
                exchangeRate = exchangeRate / 10;
            }
            else if (val1 == "USD" && val2 == "EURO")
            {
                exchangeRate = exchangeRate / 1.2m;
            }
            else if (val1 == "EURO" && val2 == "USD")
            {
                exchangeRate = exchangeRate * 1.2m;
            }

            // Perform the currency conversion
            return amount * exchangeRate;

        }


        public static string AccountTypeChecker()

        {
            string AccType;

            do
            {
                Console.WriteLine("\n");
                // Code to prompt for and validate currency input
                Console.Write("Enter Account Type: ");
                AccType = Console.ReadLine().ToUpper();

                if (AccType != "SAVING" && AccType != "CURRENT" && AccType != "CHECKING")
                {


                    Console.WriteLine("\n \u001b[31mInvalid Account Type please enter (Saving/Current/Checking)\u001b[0m");


                }
                else
                {
                    return AccType;
                }

            } while (true);
        }



        public static void EscapeKeyCall()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t\t\u001b[0m Press \u001b[34m ESC \u001b[0m to exit");

            ConsoleKey key;
            do
            {
                key = Console.ReadKey().Key;

                if (key != ConsoleKey.Escape)
                {
                    Console.WriteLine("\n\t\t\t\u001b[31m Wrong key pressed. Press \u001b[34m ESC\u001b[0m \u001b[31m to exit.\t\t\t\u001b[0m");
                }
            } while (key != ConsoleKey.Escape);
        }




    }
}
