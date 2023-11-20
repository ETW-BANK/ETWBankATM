using Etw_BANK_FINAL.Data;
using Etw_BANK_FINAL.MenuItems;
using Etw_BANK_FINAL.Methods;
using Etw_BANK_FINAL.Utilities;
using Etw_BANK_FINAL.WelcomeUI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.Logins
{
    internal class Login : Method
    {
        public static void AdminLogin()
        {
            // Header message for admin login
            string header = $"\n\n\t\t\t\t================\u001b[36m WELCOME To ETW-BANK \u001b[0m=====================\n\n" +
                $"\n\n\t\t================\u001b[36m Please ENTER Your \u001b[33m UserID \u001b[0m and \u001b[33m PIN Code \u001b[0m to \u001b[33m LOGIN\u001b[0m=====================\n\n";
            Console.WriteLine(header);
            using (EtwBankContext context = new EtwBankContext())
            {
                int maxLoginAttempts = 3;
                // Loop through login attempts
                for (int loginAttempts = 0; loginAttempts < maxLoginAttempts; loginAttempts++)
                {
                    // Prompt for admin username input
                    Console.WriteLine("\n\n\t\t\t\t\t\t \u001b[33mEnter your User Name\u001b[0m");
                    Console.Write("\n\n\t\t\t\t\t\t\t");
                    string username = Console.ReadLine().ToUpper();
                    // Check if the entered username is not for an admin
                    if (username != "ADMIN".ToUpper())
                    {
                        // Display message and continue to prompt for username
                        Console.WriteLine("\n\n");
                        Console.Clear();
                        Console.WriteLine(header);
                        Console.WriteLine($"\t\t\t\t\t     \u001b[31m User can't log in as Admin\u001b[0m");
                        continue;
                    }
                    // Securely retrieve the PIN input
                    SecureString pass = Utility1.PinHide();
                    string pin = new System.Net.NetworkCredential(string.Empty, pass).Password;
                    Console.Clear();
                    Console.WriteLine(header);
                    Console.WriteLine("\n");
                    // Check admin credentials from the database
                    currentUser = context.Users
                        .Include(u => u.Accounts)
                        .FirstOrDefault(u => u.UserName == username && u.PinCode == pin);
                    if (currentUser != null)
                    {
                        Console.WriteLine("\n");
                        AdminMenues.AdminMenu();
                        return; // Return to the calling method upon successful login
                    }
                    else
                    {
                        // Display an error message for invalid credentials
                        Console.WriteLine("\n\t\t\t\t\t \u001b[31mInvalid username or PIN. Please try again.\u001b[30m");
                    }
                }
                // Display message for reaching maximum login attempts
                Console.WriteLine("\n\t\t\t\t\t\u001b[31mMaximum login attempts reached. Exiting.\u001b[30m");
                Thread.Sleep(2000);
                Console.WriteLine("\n");
                Utility1.Loading();
                Console.Clear();
                Welcome.WelcomeUI();
            }
        }

        // Represents the list of blocked users along with the block end time
        private static readonly List<(string username, DateTime blockEndTime)> blockedUsers = new List<(string, DateTime)>();

        // Maximum number of login attempts before blocking the user
        private const int MaxLoginAttempts = 3;

        // Duration for which a user remains blocked after exceeding login attempts
        private static readonly TimeSpan BlockDuration = TimeSpan.FromMinutes(5);

        // Method to handle user login functionality
        public static void UserLogin()
        {
            // Header displaying welcome and login prompt
            string header = $"\n\n\t\t\t\t================\u001b[36m WELCOME To ETW-BANK \u001b[0m=====================\n\n" +
                $"\n\n\t\t================\u001b[36m Please ENTER Your \u001b[33m UserID \u001b[0m and \u001b[33m PIN Code \u001b[0m to \u001b[33m LOGIN\u001b[0m=====================\n\n";

            Console.WriteLine(header);

            using (EtwBankContext context = new EtwBankContext())
            {
                for (int loginAttempts = 0; loginAttempts < MaxLoginAttempts; loginAttempts++)
                {
                    // Prompting the user for username input
                    Console.WriteLine("\n\n\t\t\t\t\t\t \u001b[33mEnter your User Name\u001b[0m");
                    Console.Write("\n\n\t\t\t\t\t\t\t");
                    string username = Console.ReadLine().ToUpper();

                    // Blocking login attempt for 'ADMIN'
                    if (username == "ADMIN".ToUpper())
                    {
                        Console.WriteLine("\n\n");
                        Console.Clear();
                        Console.WriteLine(header);
                        Console.WriteLine($"\t\t\t\t\t  \u001b[31m User can't log in as Admin\u001b[0m");
                        continue; // Continue the loop to prompt for another username
                    }

                    // Getting PIN input securely
                    SecureString pass = Utility1.PinHide();
                    string pin = new System.Net.NetworkCredential(string.Empty, pass).Password;

                    Console.Clear();
                    Console.WriteLine(header);
                    Console.WriteLine("\n");

                    // Checking if the user is blocked
                    if (IsUserBlocked(username))
                    {
                        Console.WriteLine("\n\t\t\t\t\t\u001b[31mYour account is blocked. Please try again later.\u001b[0m \n\n");
                        Utility1.Loading();
                        Console.Clear();
                        Welcome.WelcomeUI();
                    }

                    // Validating user credentials
                    currentUser = context.Users
                        .Include(u => u.Accounts)
                        .FirstOrDefault(u => u.UserName == username && u.PinCode == pin);

                    if (currentUser != null)
                    {
                        Console.WriteLine("\n");
                        UserMenues.UserMenu();
                        return; // Return to the calling method upon successful login
                    }
                    else
                    {
                        Console.WriteLine("\n\t\t\t\t\t \u001b[31mInvalid username or PIN. Please try again.\u001b[30m");
                    }

                    // Blocking the user if maximum login attempts reached
                    if (loginAttempts == MaxLoginAttempts - 1)
                    {
                        Console.WriteLine("\n\t\t\t\t\t\u001b[31mMaximum login attempts reached. Your account will be blocked.\u001b[0m");
                        blockUser(username);
                    }
                }

                Thread.Sleep(2000);
                Console.WriteLine("\n");
                Utility1.Loading();
                Console.Clear();
                Welcome.WelcomeUI();
            }
        }

        // Checks if the given username is blocked
        private static bool IsUserBlocked(string username)
        {
            var blockedUser = blockedUsers.FirstOrDefault(u => u.username == username);
            if (blockedUser != default && blockedUser.blockEndTime > DateTime.Now)
            {
                return true;
            }
            else if (blockedUser != default)
            {
                blockedUsers.Remove(blockedUser);
            }
            return false;
        }

        // Blocks the given username for a specified duration
        private static void blockUser(string username)
        {
            blockedUsers.Add((username, DateTime.Now.Add(BlockDuration)));
        }

    }
}
