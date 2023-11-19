using Etw_BANK_FINAL.Data;
using Etw_BANK_FINAL.MenuItems;
using Etw_BANK_FINAL.Model;
using Etw_BANK_FINAL.Utilities;
using Etw_BANK_FINAL.WelcomeUI;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.Methods
{
    internal class Method
    {
        public static User currentUser;
        public static void DepositMoney()
        {
            int accountId;
            decimal depositAmount;

            using (EtwBankContext context = new EtwBankContext())
            {
                Console.WriteLine("\n\n\t\t================ Deposit Money ================\n");


                // Validate and get account number input
                do
                {
                    Console.Write("Enter Account Number: ");

                    var inputAccountId = Console.ReadLine();

                    if (int.TryParse(inputAccountId, out accountId))
                    {
                        break; // Break the loop if a valid integer value is entered
                    }
                    else
                    {
                        Console.WriteLine("\n \u001b[31m Invalid input for Account Number. Please enter a valid integer value.\u001b[0m \n");
                    }
                } while (true);


                Console.WriteLine("\n");

                // Validate and get deposit amount input
                do
                {
                    Console.Write("Enter Deposit Amount: ");

                    var inputDepositAmount = Console.ReadLine();

                    if (decimal.TryParse(inputDepositAmount, out depositAmount) && depositAmount > 0)
                    {
                        break; // Break the loop if a valid numeric value is entered
                    }
                    else
                    {
                        Console.WriteLine("\n \u001b[31m Invalid input for Deposit Amount. Please enter a valid Amount.\u001b[0m \n");
                    }
                } while (true);

                Console.WriteLine("\n");

                // Retrieve account from the database based on the entered account ID
                var account = context.Accounts.SingleOrDefault(a => a.AccountNumber == accountId);

                if (account != null)
                {
                    // Update the account balance with the deposit amount
                    account.Balance += depositAmount;
                    context.SaveChanges();

                    Console.WriteLine("\n \u001b[32mDeposit successful.\n \u001b[0m");
                }
                else
                {
                    Console.WriteLine("\n \u001b[31m Invalid Account Number.\u001b[0m \n");
                }

                // Update the current user's account information from the database
                currentUser.Accounts = context.Accounts
                    .Where(a => a.UserId == currentUser.UserId)
                    .ToList();
            }

            Thread.Sleep(3000); // Introduce a delay for user experience

         

            UserMenues.UserMenues(); // Return to the user menu after completing the deposit process
        }

        public static void TransferMoney()
        {
            using (EtwBankContext context = new EtwBankContext())
            {
                int senderAcc, receiverAcc;
                decimal amount;

                Console.WriteLine("\n\n\t\t================ Transfer Money ================\n");


                // Validate and get sender's account input
                do
                {
                    Console.Write("Enter Sender's Account:");
                    if (!int.TryParse(Console.ReadLine(), out senderAcc))
                    {
                        Console.WriteLine("\n \u001b[31m Invalid sender account number. Please enter a valid integer value.\u001b[0m \n");
                    }
                    else
                    {
                        break; // Break the loop if a valid integer value is entered
                    }
                } while (true);

                Console.WriteLine("\n");
                // Validate and get receiver's account input
                do
                {
                    Console.Write("Enter Receiver's Account:");
                    if (!int.TryParse(Console.ReadLine(), out receiverAcc))
                    {
                        Console.WriteLine("\n \u001b[31m Invalid receiver account number. Please enter a valid integer value.\u001b[0m \n");
                    }
                    else
                    {
                        break; // Break the loop if a valid integer value is entered
                    }
                } while (true);

                Console.WriteLine("\n");
                // Validate and get amount input to transfer
                do
                {
                    Console.Write("Enter Amount To Transfer:");
                    if (!decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
                    {
                        Console.WriteLine("\n \u001b[31m Invalid amount. Please enter a valid Amount.\u001b[0m \n");
                    }
                    else
                    {
                        break; // Break the loop if a valid numeric value is entered
                    }
                } while (true);

                var senderAccount = context.Accounts.FirstOrDefault(a => a.AccountNumber == senderAcc);
                var receiverAccount = context.Accounts.SingleOrDefault(a => a.AccountNumber == receiverAcc);

                if (senderAccount != null && receiverAccount != null)
                {
                    if (senderAccount.Balance >= amount)
                    {
                        // Perform currency conversion
                        string senderCurrency = senderAccount.Currency.ToUpper();
                        string receiverCurrency = receiverAccount.Currency.ToUpper();
                        decimal exchangedAmount = Utility1.CurrencyChanger(senderCurrency, receiverCurrency, amount);

                        // Deduct the original amount from the sender's account
                        senderAccount.Balance -= amount;

                        // Add the exchanged amount to the receiver's account
                        receiverAccount.Balance += exchangedAmount;

                        // Save changes to the database
                        context.SaveChanges();

                        // Output success message
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n \u001b[32mTransfer successful.\u001b[0m \n");
                        Console.ResetColor();
                    }
                    else
                    {
                        // Handle insufficient balance in sender's account
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n \u001b[31m Insufficient balance in the sender's account.\u001b[0m \n");
                        Console.ResetColor();
                    }
                }
                else
                {
                    // Handle invalid account numbers
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n \u001b[31m Invalid account number. Please check either Sender's/Receiver's Account Number \u001b[0m \n");
                    Console.ResetColor();
                }

                Thread.Sleep(2000);
                UserMenues.UserMenues();
            }
        }

        public static void WithdrawMoney()
        {
            using (EtwBankContext context = new EtwBankContext())
            {
                Console.WriteLine("\n\n\t\t================ Withdraw Money ================\n");
                int accountId;
                decimal withdrawalAmount;
                do
                {
                    Console.Write("Enter Account Number: ");
                    string inputAccountId = Console.ReadLine();
                    if (int.TryParse(inputAccountId, out accountId))
                    {
                        break; // Break the loop if a valid integer value is entered
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("\n \u001b[31m Invalid input for Account Number. Please enter a valid integer value.\u001b[0m \n");
                    }
                } while (true);
                Console.WriteLine("\n");
                do
                {
                    Console.Write("Enter Withdrawal Amount: ");
                    string inputWithdrawalAmount = Console.ReadLine();
                    if (decimal.TryParse(inputWithdrawalAmount, out withdrawalAmount) && withdrawalAmount > 100)
                    {
                        break; // Break the loop if a valid numeric value is entered and minimum amount is met.
                    }
                    else
                    {
                        Console.WriteLine("\n \u001b[31m Invalid input for Withdrawal Amount. Please enter a valid Amount (Minimum Amount is 100).\u001b[0m \n");
                    }
                } while (true);
                var account = context.Accounts.SingleOrDefault(a => a.AccountNumber == accountId);
                if (account != null)
                {
                    if (account.Balance >= withdrawalAmount)
                    {
                        // Update account balance after withdrawal
                        account.Balance -= withdrawalAmount;
                        context.SaveChanges();
                        Console.WriteLine("\n \u001b[32m Withdrowal Sucessfull.\u001b[0m \n");
                    }
                    else
                    {
                        Console.WriteLine("\n \u001b[31m Insufficient balance in the Account.\u001b[0m \n");
                    }
                }
                else
                {
                    Console.WriteLine("\n \u001b[31m Invalid Account Number. \u001b[0m \n");
                }
                currentUser.Accounts = context.Accounts
              .Where(a => a.UserId == currentUser.UserId).ToList();
            }
            Thread.Sleep(1000);
            Utility1.Loading();
            UserMenues.UserMenu();
        }

        public static void Logout()
        {
            Utility1.Loading(); // Display a loading indicator or perform any necessary cleanup tasks before logging out
            currentUser = null;    // Reset the current user to null, effectively logging them out
            Console.WriteLine("Logged out successfully.");    // Display a message confirming successful logout
            Console.Clear();      // Clear the console screen after logout
            Welcome.WelcomeUI(); // Navigate back to the welcome interface or initial menu
        }
    }
}
